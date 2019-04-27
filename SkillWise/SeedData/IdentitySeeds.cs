using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SkillWise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillWise.SeedData
{
    public static class IdentitySeeds
    {
        private const string testUser = "Test";
        private const string testPassword = "P@ssword123";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));

                IdentityUser user = await userManager.FindByIdAsync(testUser);

                if (user == null)
                {
                    user = new IdentityUser("Test");
                    await userManager.CreateAsync(user, testPassword);
                }
            }
        }
    }
}
