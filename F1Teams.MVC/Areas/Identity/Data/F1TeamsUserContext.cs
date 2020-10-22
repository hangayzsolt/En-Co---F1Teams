using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F1Teams.MVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace F1Teams.MVC.Data
{
    public class F1TeamsUserContext : IdentityDbContext<F1User>
    {
        public F1TeamsUserContext(DbContextOptions<F1TeamsUserContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
