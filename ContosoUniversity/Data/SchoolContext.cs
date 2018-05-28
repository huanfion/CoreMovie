using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    public class SchoolContext:DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //EF 约定
            //DbSet 类型的属性用作表名。 如果实体未被 DbSet 属性引用，实体类名称用作表名称。
            //使用实体属性名作为列名
            //以 ID 或 classnameID 命名的实体属性被视为主键属性
            //如果属性名为 将被解释为外键属性 (例如， StudentID 对应 Student 导航属性， Student 实体的主键是ID ，所以 StudentID 被解释为外键属性)。 此外也可以将外键属性命名为(例如， EnrollmentID ，由于Enrollment 实体的主键是 EnrollmentID ，因此被解释为外键)。
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            
        }
    }
}
