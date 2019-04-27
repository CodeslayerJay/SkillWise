using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkillWise.Models;

namespace SkillWise.Controllers
{
    public class SkillHourController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SkillHourController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string Index()
        {
            return "The index page has not been implemented yet.";
        }

        // Get: /skillhour/create/:skillid
        public IActionResult LogHours(int? id)
        {
            if( id == null)
            {
                return NotFound();
            }

            ViewData["SkillId"] = id;

            return View();
        }
        
        // Post LogHours
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogHours(SkillHour skillHour)
        {

            if (ModelState.IsValid)
            {
                _dbContext.SkillHours.Add(skillHour);
                _dbContext.SaveChanges();

                return RedirectToAction("View", "Skill", new { id = skillHour.SkillId });
            }

            ViewData["SkillId"] = skillHour.SkillId;
            return View(skillHour);
        }

    }
}