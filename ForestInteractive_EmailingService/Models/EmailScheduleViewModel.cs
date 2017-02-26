using ForestInteractive_EmailingService.Models.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForestInteractive_EmailingService.Models
{
    [MustPassDateOrNow]
    public class EmailScheduleViewModel
    {
        public EmailScheduleViewModel()
        { }

        public EmailScheduleViewModel(EmailSchedule model)
        {
            Id = model.Id;
            SendDate = model.SendDateTime;
            SendTime = model.SendDateTime;
            EmailBody = model.EmailBody;
            EmailSubject = model.EmailSubject;
            FromEmail = model.FromEmail;
            Recipients = model.Recipients;
        }

        public int Id { get; set; }

        [Display(Name = "Send Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SendDate { get; set; }

        [CompareSendDateTimeWithNowValidationAttribute(15, DateTimeCompareTo.After, ErrorMessage = "{0}must be {1} ,{2}.")]
        [Display(Name = "Send Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime? SendTime { get; set; }

        [Display(Name = "Don't schedule and send them now.")]
        public bool SendingNow { get; set; }

        [Display(Name = "Use my current Email address.")]
        public bool UseMyEmail { get; set; }

        [Required]
        [Display(Name = "Email Body")]
        [DataType(DataType.MultilineText)]
        [RegularExpression(@"^[^$#@&%€]*$", ErrorMessage = "The {0} must not include $, #, @, &, % or € charecter.")]
        [StringLength(160, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 20)]
        public string EmailBody { get; set; }

        [Required]
        [Display(Name = "Email Subject")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string EmailSubject { get; set; }

        [Required]
        [Display(Name = "Sender Email Address")]
        [EmailAddress]
        public string FromEmail { get; set; }

        [Display(Name = "To Email Addresses")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = ".csv", ErrorMessage = "The {0} for upload must be in csv format.")]
        public byte[] Recipients { get; set; }

    }
}