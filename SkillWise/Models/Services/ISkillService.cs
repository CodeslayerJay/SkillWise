using SkillWise.Models.ViewModels;
using System.Collections.Generic;

namespace SkillWise.Models.Services
{
    public interface ISkillService
    {
        Skill GetById(int skillId);
        void Save(Skill skill);
        IEnumerable<Skill> GetAll(string UserId);
        void Delete(Skill skill);
        void Update(Skill skill);
        double GetSumOfHours(int skillId);
        void AddTask(SkillTask skillTask);
        SkillTask GetTaskById(int id);
        SkillTask SetTaskToComplete(int id, string userId);
        SkillTask UpdateTask(SkillTask skillTask);
        int? DeleteTask(int id, string userId);
    }
}