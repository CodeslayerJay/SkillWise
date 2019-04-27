using Microsoft.EntityFrameworkCore;
using SkillWise.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillWise.Models.Services
{
    public class SkillService : ISkillService
    {
        private ApplicationDbContext _dbContext;

        public SkillService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Skill GetById(int skillId)
        {
            var result = _dbContext.Skills.Where(s => s.SkillId == skillId).FirstOrDefault();

            if (result == null)
            { 
                return null;
            }

            // Get any skill tasks associated

            result.SkillTasks = _dbContext.SkillTasks.Where(t => t.SkillId == skillId)
                .OrderByDescending(x => x.IsComplete)
                .OrderBy(x=>x._timestamp)
                .OrderByDescending(x => x.IsFundamental)
                .OrderByDescending(x => x.IsPriority)
                .ToList();

            result.SkillHours = _dbContext.SkillHours.Where(x => x.SkillId == skillId).ToList();

            return result;
            
        }

        public IEnumerable<Skill> GetAll(string userId)
        {
            var result = _dbContext.Skills.Where(s => s.UserId == userId).ToList();
            
            return result;
        }

        // Save new skill
        public void Save(Skill skill)
        {
            if( skill != null)
            {
                
                _dbContext.Skills.Add(skill);
                _dbContext.SaveChanges();
            }
        }

        // Update
        public void Update(Skill skill)
        {
            if(skill != null)
            {
                //var skillToUpdate = _dbContext.Skills.Where(s => s.SkillId == skill.SkillId).First();

                
                _dbContext.Skills.Update(skill);
                _dbContext.SaveChanges();
            }
        }

        // Delete skill
        public void Delete(Skill skill)
        {
            if( skill != null)
            {
                _dbContext.Skills.Remove(skill);
                _dbContext.SaveChanges();
            }
        }

        // Get sum of total logged hours
        public double GetSumOfHours(int skillId)
        {
            var hourLogs = _dbContext.SkillHours.Where(x => x.SkillId == skillId).ToList();
            double total = 0;

            if (hourLogs != null)
            {
                foreach(var log in hourLogs)
                {
                    total = total + double.Parse(log.Hours);
                }

                return total;
                
            }

            return total;
        }

        // Add task to skill
        public void AddTask(SkillTask skillTask)
        {
            var skill = _dbContext.Skills.Where(x => x.SkillId == skillTask.SkillId).First();

            if( skill != null)
            {
                _dbContext.SkillTasks.Add(skillTask);
                _dbContext.SaveChanges();
            }
        }

        // Get Task by Id
        public SkillTask GetTaskById(int id)
        {
            var result = _dbContext.SkillTasks.Where(x => x.SkillTaskId == id)
                .Include(s=>s.Skill)
                .First();

            return result;
        }

        // Set task to complete
        public SkillTask SetTaskToComplete(int id, string userId)
        {
            var skillTask = _dbContext.SkillTasks.Where(x => x.SkillTaskId == id)
                .Include(s => s.Skill).First();

            if( skillTask != null)
            {
                if(skillTask.Skill.UserId == userId)
                {
                    skillTask.IsComplete = true;
                    _dbContext.SkillTasks.Update(skillTask);
                    _dbContext.SaveChanges();
                }

                return skillTask;
            }

            return null;
        }

        // Set task to complete
        public SkillTask UpdateTask(SkillTask skillTask)
        {
            if (skillTask != null)
            {
                skillTask.IsComplete = false;
                _dbContext.SkillTasks.Update(skillTask);
                _dbContext.SaveChanges();

                return skillTask;
            }

            return null;
        }

        // Delete Task / return skill id
        public int? DeleteTask(int id, string userId)
        {
            var task = _dbContext.SkillTasks.Where(x => x.SkillTaskId == id)
                .Include(s => s.Skill)
                .First();
            
            if( task != null)
            {
                int skillId = task.SkillId;

                if( task.Skill.UserId == userId)
                {
                    _dbContext.SkillTasks.Remove(task);
                    _dbContext.SaveChanges();
                }

                return skillId;
            }

            return null;
        }

    }

}
