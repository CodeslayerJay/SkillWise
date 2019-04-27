using SkillWise.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillWise.Models.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {

        }

        /// <summary>
        /// Build a new ApplicationUser Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ApplicationUser</returns>
        public ApplicationUser BuildUser(SignUpViewModel model)
        {
            // Build new user
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            return user;
        }
    }
}
