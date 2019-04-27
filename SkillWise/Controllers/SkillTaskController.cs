using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillWise.Models;
using SkillWise.Models.Services;

namespace SkillWise.Controllers
{
    public class SkillTaskController : Controller
    {
        private readonly ISkillService _skillService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SkillTaskController(ISkillService skillService, UserManager<ApplicationUser> userManager)
        {
            _skillService = skillService;
            _userManager = userManager;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int id)
        {
            ViewData["SkillId"] = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SkillTask skillTask)
        {
            if (ModelState.IsValid)
            {
                _skillService.AddTask(skillTask);
                return RedirectToAction("View", "Skill", new { id = skillTask.SkillId });
            }

            ViewData["SkillId"] = skillTask.SkillId;
            return View(skillTask);
            
        }


        public IActionResult Details(int id)
        {
            var model = _skillService.GetTaskById(id);

            if( model == null)
            {
                return NotFound();
            }

            if(model.Skill.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }


            return View(model);
        }

        public IActionResult SetComplete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var task = _skillService.SetTaskToComplete(id, userId);
            
            if(task == null)
            {
                return NotFound();
            }

            return RedirectToAction("View", "Skill", new { id = task.SkillId });
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTask(SkillTask skillTask)
        {
            if (ModelState.IsValid)
            {
                var result = _skillService.UpdateTask(skillTask);

                if(result == null)
                {
                    return NotFound();
                }

                return RedirectToAction("View", "Skill", new { id = skillTask.SkillId });
            }

            return View(skillTask);
        }

        public IActionResult DeleteTask(int id)
        {
            var userId = _userManager.GetUserId(User);
            var skillId = _skillService.DeleteTask(id, userId);

            if(skillId == null)
            {
                return NotFound();
            }

            return RedirectToAction("View", "Skill", new { id = skillId });
        }

    }
}