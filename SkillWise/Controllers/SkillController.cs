using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillWise.Models;
using SkillWise.Models.Services;
using SkillWise.Models.ViewModels;

namespace SkillWise.Controllers
{
    public class SkillController : Controller
    {
        private readonly ISkillService _skillService;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public SkillController(ISkillService skillService, UserManager<ApplicationUser> userManager)
        {
            _skillService = skillService;
            _userManager = userManager;
            
        }
        

        // Get: Details view
        public IActionResult View(int id)
        {
            
            var model = _skillService.GetById(id);
            var userId = _userManager.GetUserId(User);

            if( model == null || model.UserId != userId)
            {
                return NotFound();
            }

            ViewData["SumOfHours"] = _skillService.GetSumOfHours(id).ToString();
            return View(model);
        }

        // Get: Create
        public IActionResult Create()
        {
            return View();
        }

        // Post: create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Skill skill)
        {
            if (ModelState.IsValid)
            {

                var userId = _userManager.GetUserId(User);
                skill.UserId = userId;
                _skillService.Save(skill);

                return RedirectToAction("Index", "Dashboard");

            }

            return View();
        }

        // Get: Details
        public IActionResult Details(int id)
        {
            var model = _skillService.GetById(id);
            var userId = _userManager.GetUserId(User);

            if(model == null || userId != model.UserId)
            {
                return NotFound();
            }


            return View(model);
        }

        // Post: update details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateDetails(Skill skill)
        {
            if (ModelState.IsValid)
            {
                skill.UserId = _userManager.GetUserId(User);
                _skillService.Update(skill);
                return RedirectToAction("View", "Skill", new { id = skill.SkillId });

            }

            return View();
        }

        // Get: Confirm delete page {skillid}
        public IActionResult ConfirmDelete(int id)
        {
            var model = _skillService.GetById(id);
            var userId = _userManager.GetUserId(User);

            if( model == null || model.UserId != userId )
            {
                return NotFound();
            }

            return View(model);
        }

        // Post: confirm delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Skill model)
        {
            var userId = _userManager.GetUserId(User);

            _skillService.Delete(model);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}