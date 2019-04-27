using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillWise.Models
{
    public class Skill
    {
        public int SkillId { get; set; }

        
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Please enter the name of the skill.")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        
        [StringLength(100, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Display(Name = "Public")]
        public bool IsPublic { get; set; }

        [Display(Name = "Experience")]
        public Experience Experience { get; set; }

        public DateTime _timestamp { get; set; } = DateTime.Now;

        // Relationships

        // User
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        // Assignments / Tasks
        public virtual List<SkillTask> SkillTasks { get; set; }
        public virtual List<SkillHour> SkillHours { get; set; }
    }

    public enum Experience
    {
        Student, Moderate, Expert
    }
}
