using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;

namespace SchoolManagement.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Department>().ToTable("Departments");
        modelBuilder.Entity<Instructor>().ToTable("Instructors");
        modelBuilder.Entity<Course>().ToTable("Courses");
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Enrollment>().ToTable("Enrollments");

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Department)
            .WithMany(d => d.Courses)
            .HasForeignKey(c => c.DepartmentID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany()
            .HasForeignKey(e => e.CourseID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Department>().Property(d => d.Budget).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Enrollment>().Property(e => e.Grade).HasColumnType("decimal(3,2)");
    }
}
