using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillWise.Models.Services
{
    public class SkillTaskService
    {
        private ApplicationDbContext _dbContext;

        public SkillTaskService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<SkillTask> GetSkillTasks(int skillId)
        {
            return _dbContext.SkillTasks.Where(x => x.SkillId == skillId)
                .OrderByDescending(x => x.IsFundamental)
                .OrderByDescending(x => x.IsPriority)
                .ToList();
        }

    }
}
