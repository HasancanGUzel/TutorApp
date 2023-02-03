using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OzelDers.Data.Config;
using OzelDers.Data.Extensions;
using OzelDers.Entity.Concrete;
using OzelDers.Entity.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OzelDers.Data.Concrete.EfCore.Context
{
    public class OzelDersContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<LessonBranch> LessonsBranches { get; set; }
        public DbSet<TeacherLesson> TeachersLessons { get; set; }
        public DbSet<StudentLesson> StudentsLessons { get; set; }
        public DbSet<Comment> Comments { get; set; } //-------------------------
        public OzelDersContext(DbContextOptions<OzelDersContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
            modelBuilder.ApplyConfiguration(new BranchConfig());
            modelBuilder.ApplyConfiguration(new LessonBranchConfig());
            modelBuilder.ApplyConfiguration(new LessonConfig());
            modelBuilder.ApplyConfiguration(new TeacherConfig());
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new TeacherLessonConfig());
            modelBuilder.ApplyConfiguration(new StudentLessonConfig());
            base.OnModelCreating(modelBuilder);

        }
    }
}