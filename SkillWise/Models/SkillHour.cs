using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SkillWise.Models
{
    public class SkillHour
    {
        public int SkillHourId { get; set; }

        [DataType(DataType.Duration)]
        [Required]        
        public string Hours { get; set; }

        [StringLength(5000)]
        public string Note { get; set; }

        public DateTime _timestamp { get; set; } = DateTime.Now;

        [ForeignKey("Skill")]
        public int SkillId { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
