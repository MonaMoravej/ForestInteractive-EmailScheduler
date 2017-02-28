using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForestInteractive_EmailingService.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using Hangfire;
using System.Net.Mail;
using System.Configuration;
using System.IO;

namespace ForestInteractive_EmailingService.Controllers
{
    [Authorize]
    public class EmailScheduleController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public EmailScheduleController()
        {

        }
        public EmailScheduleController(ApplicationUserManager userManager)
        {

            _userManager = userManager;
        }



        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View(db.EmailSchedules.AsEnumerable().Select(entity => new EmailScheduleViewModel(entity)));
        }

        public async Task<ActionResult> Create()
        {

            ViewBag.UserEmail = await GetUserInfo(u => u.Email);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmailScheduleViewModel emailScheduleViewModel)
        {
            if (Request.Files.Count == 0 || Path.GetExtension(Request.Files[0].FileName).ToLower() != ".csv")
                ModelState.AddModelError("Recipients", "Choose a valid CSV file for recipients");
            else if (ModelState.IsValid)
            {
                using (var ms = new MemoryStream())
                {
                    Request.Files[0].InputStream.CopyTo(ms);
                    emailScheduleViewModel.Recipients = ms.ToArray();
                }
                EmailSchedule entity = await MapToEmailSchadle(emailScheduleViewModel);

                if (!EmailScheduleISDuplicate(entity))
                {

                    db.EmailSchedules.Add(entity);
                    db.SaveChanges();
                    try
                    {
                        entity.JobId = BackgroundJob.Schedule(() => sendEmail(entity.Id), new DateTimeOffset(entity.SendDateTime));
                    }
                    catch
                    {
                        db.EmailSchedules.Remove(entity);
                    }
                    finally
                    {
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", new { message = "Sending Email was scheduled on " + entity.SendDateTime.ToString() });
                }
                ModelState.AddModelError("", "You can not send a same massage in one day.");
            }

            return View(emailScheduleViewModel);
        }


        public ActionResult Cancel(int id)
        {
            var schedule = db.EmailSchedules.Find(id);
            return View(new EmailScheduleViewModel(schedule));
        }

        [HttpPost]
        [ActionName("Cancel")]
        public ActionResult CancelConfirmation(EmailScheduleViewModel emailScheduleViewModel)
        {
            var schedule = db.EmailSchedules.Find(emailScheduleViewModel.Id);
            db.EmailSchedules.Remove(schedule);
            BackgroundJob.Delete(schedule.JobId);
            db.SaveChanges();

            return RedirectToAction("Index", new { message = $"Email scheduled for {schedule.SendDateTime} cancelled successfully" });
        }

        [NonAction]
        public void sendEmail(int id)
        {
            var schedule = db.EmailSchedules.Find(id);

            string recipients = "";
            using (var ms = new MemoryStream(schedule.Recipients))
            {
                recipients = new StreamReader(ms).ReadToEnd().Replace("\r", "").Replace("\n", "").TrimEnd(',');
            }

            var smtp = new SmtpClient(ConfigurationManager.AppSettings["SMTPHost"], int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUser"], ConfigurationManager.AppSettings["SMTPPassword"]);
            var message = new MailMessage { From = new MailAddress(schedule.FromEmail), Subject = schedule.EmailSubject, Body = schedule.EmailBody };
            message.To.Add(recipients);
            smtp.Send(message);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }

            }
            base.Dispose(disposing);
        }

        private async Task<EmailSchedule> MapToEmailSchadle(EmailScheduleViewModel viewModel)
        {
            EmailSchedule entity = new EmailSchedule();

            if (viewModel.SendingNow) entity.SendDateTime = DateTime.Now;
            else entity.SendDateTime = viewModel.SendDate.Value.Date + viewModel.SendTime.Value.TimeOfDay;

            if (viewModel.UseMyEmail) entity.FromEmail = await GetUserInfo(u => u.Email);
            else entity.FromEmail = viewModel.FromEmail;

            entity.EmailBody = viewModel.EmailBody;
            entity.EmailSubject = viewModel.EmailSubject;
            entity.Recipients = viewModel.Recipients;
            entity.UserId = await GetUserInfo(u => u.Id);

            return entity;
        }

        //u=>u.UserId or u=> u.Email
        private async Task<string> GetUserInfo(Func<ApplicationUser, string> fieldSelector)
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            return fieldSelector(user);
        }

        private bool EmailScheduleISDuplicate(EmailSchedule entity)
        {
            DateTime selectedDate = entity.SendDateTime.Date;
            DateTime start = new DateTime(entity.SendDateTime.Year, entity.SendDateTime.Month, entity.SendDateTime.Day, 0, 0, 0, 0, DateTimeKind.Unspecified);
            DateTime end = new DateTime(entity.SendDateTime.Year, entity.SendDateTime.Month, entity.SendDateTime.Day, 23, 59, 59, 0, DateTimeKind.Unspecified);

            return db.EmailSchedules.Count(e => e.EmailSubject.Trim().ToLower() == entity.EmailSubject.Trim().ToLower() && e.SendDateTime > start && e.SendDateTime < end) > 0;
        }

    }
}
