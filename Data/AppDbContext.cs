using Microsoft.EntityFrameworkCore;
using TrainingCenter.Entities;

namespace TrainingCenter.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Enrollment> Enrollments { get; set; }
    public virtual DbSet<Instructor> Instructors { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<StudentProfile> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // =========================
        // Course
        // =========================
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasIndex(e => e.InstructorId);
            entity.HasIndex(e => e.Status);

            entity.HasIndex(e => e.Code)
                  .IsUnique();

            entity.Property(e => e.Code)
                  .HasMaxLength(30);

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.Description)
                  .HasMaxLength(500);

            entity.Property(e => e.Level)
                  .HasMaxLength(30);

            entity.Property(e => e.Price)
                  .HasPrecision(10, 2);

            entity.Property(e => e.Status)
                  .HasMaxLength(20);

            entity.Property(e => e.Title)
                  .HasMaxLength(150);

            entity.HasOne(d => d.Instructor)
                  .WithMany(p => p.Courses)
                  .HasForeignKey(d => d.InstructorId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // =========================
        // Enrollment
        // =========================
        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasIndex(e => e.CourseId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.StudentId);

            entity.HasIndex(e => new { e.StudentId, e.CourseId })
                  .IsUnique();

            entity.Property(e => e.EnrollmentDate)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.FinalGrade)
                  .HasPrecision(5, 2);

            entity.Property(e => e.ProgressPercentage)
                  .HasPrecision(5, 2);

            entity.Property(e => e.Status)
                  .HasMaxLength(20);

            entity.HasOne(d => d.Course)
                  .WithMany(p => p.Enrollments)
                  .HasForeignKey(d => d.CourseId);

            entity.HasOne(d => d.Student)
                  .WithMany(p => p.Enrollments)
                  .HasForeignKey(d => d.StudentId);
        });

        // =========================
        // Instructor
        // =========================
        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasIndex(e => e.ManagerId);

            entity.HasIndex(e => e.Email)
                  .IsUnique();

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.Property(e => e.IsActive)
                  .HasDefaultValue(true);

            entity.Property(e => e.Salary)
                  .HasPrecision(10, 2);

            entity.HasOne(d => d.Manager)
                  .WithMany(p => p.ManagedInstructors)
                  .HasForeignKey(d => d.ManagerId);
        });

        // =========================
        // Student
        // =========================
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.Status);

            entity.HasIndex(e => e.Email)
                  .IsUnique();

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);

            entity.Property(e => e.RegisteredAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.Status)
                  .HasMaxLength(20);
        });

        // =========================
        // StudentProfile
        // =========================
        modelBuilder.Entity<StudentProfile>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.Property(e => e.StudentId)
                  .ValueGeneratedNever();

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Bio).HasMaxLength(500);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.LinkedInUrl).HasMaxLength(200);

            entity.HasOne(d => d.Student)
                  .WithOne(p => p.Profile)
                  .HasForeignKey<StudentProfile>(d => d.StudentId);
        });
    }
}
