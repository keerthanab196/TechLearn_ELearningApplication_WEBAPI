using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_LearningApp_WEBAPI.Models;

public partial class ELearningProjectDbContext : DbContext
{
    public ELearningProjectDbContext()
    {
    }

    public ELearningProjectDbContext(DbContextOptions<ELearningProjectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseContent> CourseContents { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8VA4NS9\\SQLEXPRESS;Database=E_LearningProjectDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Courses_Users");
        });

        modelBuilder.Entity<CourseContent>(entity =>
        {
            entity.HasKey(e => e.ContentId);

            entity.ToTable("CourseContent");

            entity.Property(e => e.ContentId).HasColumnName("ContentID");
            entity.Property(e => e.ContentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Filepath).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Course).WithMany(p => p.CourseContents)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseContent_Courses");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
            entity.Property(e => e.CompletionDate).HasColumnType("datetime");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");
            entity.Property(e => e.Progress).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollments_Courses");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Courses");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "UC_Email").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_UserRoles");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
