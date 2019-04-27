using SkillWise.Models.ViewModels;

namespace SkillWise.Models.Services
{
    public interface IUserService
    {
        ApplicationUser BuildUser(SignUpViewModel model);
    }
}