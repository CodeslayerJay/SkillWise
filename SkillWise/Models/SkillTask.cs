using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillWise.Models
{
    public class SkillTask
    {
        [Key]
        public int SkillTaskId { get; set; }

        [StringLength(50)]
        [Required]
        [DataType(DataType.Text)]
        public string Task { get; set; }

        [StringLength(500)]
        [DataType(DataType.Text)]
        public string Note { get; set; }

        [Display(Name = "Complete")]
        public bool IsComplete { get; set; }

        [Display(Name = "Repeat Options")]
        public TaskRepeatOptions RepeatOptions { get; set; }
        
        [BindNever]
        public int ListOrder { get; set; }

        [Display(Name = "Priority")]
        public bool IsPriority { get; set; }

        [Display(Name ="Fundamental")]
        public bool IsFundamental { get; set; }

        public DateTime _timestamp { get; set; } = DateTime.Now;

        // Relationships
        public int SkillId { get; set; }
        public virtual Skill Skill { get; set; }

    }

    public enum TaskRepeatOptions
    {
        None, Daily, Weekly, Monthly
    }
}
