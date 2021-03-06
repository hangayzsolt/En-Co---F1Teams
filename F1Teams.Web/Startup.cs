using AutoMapper;
using F1Teams.BL.Implementations;
using F1Teams.BL.Interfaces;
using F1Teams.BL.Mappings;
using F1Teams.DAL.Implementations;
using F1Teams.DAL.Interfaces;
using F1Teams.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace F1Teams.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var inMemoryTeams = new SqliteConnection(Configuration.GetConnectionString("TeamsConnection"));
            inMemoryTeams.Open();
            services.AddDbContext<TeamsDbContext>(options =>
                options//.UseInMemoryDatabase("F1Teams")
                    .UseSqlite(inMemoryTeams, builder => builder.MigrationsHistoryTable("__EFMigrationsHistory")));

            var inMemoryApp = new SqliteConnection(Configuration.GetConnectionString("DefaultConnection"));
            inMemoryApp.Open();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(
                    inMemoryApp, builder => builder.MigrationsHistoryTable("__EFMigrationsHistory"));
            });
            
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAutoMapper(config => config.AddProfile(new MappingProfile()));

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

                options.SignIn.RequireConfirmedAccount = false;
            });

            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddMvcCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TeamsDbContext teamsDbContext, ApplicationDbContext applicationDbContext)
        {
            if (applicationDbContext.Database.EnsureDeleted())
            {
                applicationDbContext.Database.Migrate();

                var services = app.ApplicationServices;
                using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                userManager.CreateAsync(new IdentityUser { UserName = "admin" }, "f1teszt2018");
            }
            
            if (teamsDbContext.Database.EnsureDeleted())
            {
                teamsDbContext.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
