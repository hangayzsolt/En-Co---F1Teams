using System;
using F1Teams.MVC.Areas.Identity.Data;
using F1Teams.MVC.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(F1Teams.MVC.Areas.Identity.IdentityHostingStartup))]
namespace F1Teams.MVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<F1TeamsUserContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("F1TeamsUserContextConnection")));

                services.AddDefaultIdentity<F1User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<F1TeamsUserContext>();
            });
        }
    }
}