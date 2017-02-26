
using ForestInteractive_EmailingService.Models;
using Hangfire;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartup(typeof(ForestInteractive_EmailingService.Startup))]
namespace ForestInteractive_EmailingService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration
               .UseSqlServerStorage("DatabaseConnection");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

        }
    }
}
