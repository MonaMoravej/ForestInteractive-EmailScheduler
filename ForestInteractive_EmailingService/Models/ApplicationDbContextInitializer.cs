using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ForestInteractive_EmailingService.Models
{
    public class ApplicationDbContextInitializer
    {
        internal static void Seed(ApplicationDbContext db)
        {
            if (!(db.Users.Any()))
            {
                var passwordHash = new PasswordHasher();
                string password = passwordHash.HashPassword("Password@123");

                var userToInsert = new ApplicationUser
                {
                    //Email = "sujeevan@forest-interactive.com",
                    //UserName = "sujeevan@forest-interactive.com",

                    Email="mona.moravej@gmail.com",
                    UserName="mona.moravej@gmail.com",

                    PasswordHash = password,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                db.Users.Add(userToInsert);
                db.SaveChanges();
            }



        }

    }

}