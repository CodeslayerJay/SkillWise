using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillWise.Models;
using SkillWise.Models.Services;

namespace SkillWise.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private ISkillService _skillService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public DashboardController(ISkillService skillService, SignInManager<ApplicationUser> signInManager)
        {
            _skillService = skillService;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var userId = _signInManager.UserManager.GetUserId(User);
            var model = _skillService.GetAll(userId);
            
            return View(model);
        }

        
    }
}