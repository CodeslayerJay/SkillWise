﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillWise.Models;
using SkillWise.Models.Services;
using SkillWise.SeedData;

namespace SkillWise
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

            // Services
            services.AddSingleton<IUserService, UserService>();
            services.AddTransient<ISkillService, SkillService>();

            // Cookie Configuration
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            // DAL (Data-Access Layer)
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LocalDefaultConnection")));

            // Session State and Memory Cache
            // Session API is in the HTTPContext Namespace
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext.session?view=aspnetcore-2.2
            services.AddMemoryCache();
            services.AddSession(options => {
                options.Cookie.Name = ".SkillWiseApp.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.IsEssential = true;
            });

            // ASP.NET Identity Framework
            // For UserManagement, IdentityClaims Services, Etc
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure the options for Identity
            // Password, Email, Lockouts, etc.
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            });


            // Services to run as a MVC App
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();
            app.UseAuthentication();

            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });

            //IdentitySeeds.EnsurePopulated(app);
        }
    }
}
