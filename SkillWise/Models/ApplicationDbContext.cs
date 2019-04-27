using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillWise.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillTask> SkillTasks { get; set; }
        public DbSet<SkillHour> SkillHours { get; set; }
    }
}
