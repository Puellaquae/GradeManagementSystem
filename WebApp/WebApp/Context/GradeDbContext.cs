﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApp.Models;

#nullable disable

namespace WebApp.Context
{
    public partial class GradeDbContext : DbContext
    {

        public GradeDbContext()
        {
        }

        public GradeDbContext(DbContextOptions<GradeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<ClassCourse> ClassCourse { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Major> Major { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentCourse> StudentCourse { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<TeacherCourse> TeacherCourse { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<StudentFull> StudentFull { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Shentukq_Class18");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Id18")
                    .IsFixedLength(true);

                entity.Property(e => e.MajorId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_MajorId18")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Name18")
                    .IsFixedLength(true);

                entity.HasOne(d => d.MajorNavigation)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Shentukq_Class18_Shentukq_Major18");
            });

            modelBuilder.Entity<ClassCourse>(entity =>
            {
                entity.HasKey(e => new {e.CourseId, e.ClassId});

                entity.ToTable("Shentukq_ClassCourse18");

                entity.HasIndex(e => new {StkqClassId18 = e.ClassId, StkqCourseId18 = e.CourseId }, "IX_Shentukq_ClassCourse18")
                    .IsUnique();

                entity.Property(e => e.ClassId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_ClassId18")
                    .IsFixedLength(true);

                entity.Property(e => e.CourseId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_CourseId18")
                    .IsFixedLength(true);

                entity.HasOne(d => d.ClassNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Shentukq_ClassCourse18_Shentukq_Class18");

                entity.HasOne(d => d.CourseNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Shentukq_ClassCourse18_Shentukq_Course18");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Shentukq_Course18");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Id18")
                    .IsFixedLength(true);

                entity.Property(e => e.Credit).HasColumnName("stkq_Credit18");

                entity.Property(e => e.ExamMethod)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_ExamMethod18")
                    .IsFixedLength(true);

                entity.Property(e => e.Hours).HasColumnName("stkq_Hours18");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Name18")
                    .IsFixedLength(true);

                entity.Property(e => e.Semester)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Semester18")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Shentukq_Major18");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Id18")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Name18")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_Shentukq_Student18_1");

                entity.ToTable("Shentukq_Student18");

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .HasColumnName("stkq_UserId18")
                    .IsFixedLength(true);

                entity.Property(e => e.ClassId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_ClassId18")
                    .IsFixedLength(true);

                entity.Property(e => e.Credit)
                    .HasColumnName("stkq_Credit18")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SourcePlace)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_SourcePlace18")
                    .IsFixedLength(true);

                entity.HasOne(d => d.ClassNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Shentukq_Student18_Shentukq_Class18");

                entity.HasOne(d => d.UserNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Shentukq_Student18_Shentukq_User18");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new {e.CourseId, e.StudentId});

                entity.ToTable("Shentukq_StudentCourse18");

                entity.HasIndex(e => new {StkqStudentId18 = e.StudentId, StkqCourseId18 = e.CourseId }, "IX_Shentukq_StudentCourse18")
                    .IsUnique();

                entity.Property(e => e.CourseId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_CourseId18")
                    .IsFixedLength(true);

                entity.Property(e => e.Score).HasColumnName("stkq_Score18");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_StudentId18")
                    .IsFixedLength(true);

                entity.HasOne(d => d.CourseNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Shentukq_StudentCourse18_Shentukq_Course18");

                entity.HasOne(d => d.StudentNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Shentukq_StudentCourse18_Shentukq_Student18");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Shentukq_Teacher18");

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .HasColumnName("stkq_UserId18")
                    .IsFixedLength(true);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Telephone18")
                    .IsFixedLength(true);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Title18")
                    .IsFixedLength(true);

                entity.HasOne(d => d.UserNavigation)
                    .WithOne(p => p.Teacher)
                    .HasForeignKey<Teacher>(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Shentukq_Teacher18_Shentukq_User18");
            });

            modelBuilder.Entity<TeacherCourse>(entity =>
            {
                entity.HasKey(e => new {e.CourseId, e.TeacherId});

                entity.ToTable("Shentukq_TeacherCourse18");

                entity.HasIndex(e => new {StkqTeacherId18 = e.TeacherId, StkqCourseId18 = e.CourseId }, "IX_Shentukq_TeacherCourse18")
                    .IsUnique();

                entity.Property(e => e.CourseId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_CourseId18")
                    .IsFixedLength(true);

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_TeacherId18")
                    .IsFixedLength(true);

                entity.HasOne(d => d.CourseNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Shentukq_TeacherCourse18_Shentukq_Course18");

                entity.HasOne(d => d.TeacherNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Shentukq_TeacherCourse18_Shentukq_Teacher18");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Shentukq_User18");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Id18")
                    .IsFixedLength(true);

                entity.Property(e => e.Age).HasColumnName("stkq_Age18");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Gender18")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Name18")
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("stkq_Password18")
                    .IsFixedLength(true);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("stkq_Role18")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<StudentFull>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("StudentFull");

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.ClassId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Major)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.MajorId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.SourcePlace)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfigServices.Configuration["SqlConnectString"]);
        }
    }
}