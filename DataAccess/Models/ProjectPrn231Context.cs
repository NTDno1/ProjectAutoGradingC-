using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Models
{
    public partial class ProjectPrn231Context : DbContext
    {
        public ProjectPrn231Context()
        {
        }

        public ProjectPrn231Context(DbContextOptions<ProjectPrn231Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionDetail> QuestionDetails { get; set; } = null!;
        public virtual DbSet<QuestionNo> QuestionNos { get; set; } = null!;
        public virtual DbSet<TestCaseDb> TestCaseDbs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server =localhost; database = ProjectPrn231;uid=sa;pwd=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("date");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Question");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.UpdateDate).HasColumnType("date");

                entity.HasOne(d => d.QuestionNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_QuestionDetail");

                entity.HasOne(d => d.Student)
                    .WithMany()
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mark_User");
            });

            modelBuilder.Entity<QuestionDetail>(entity =>
            {
                entity.HasKey(e => e.QuestionId);

                entity.ToTable("QuestionDetail");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Note).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("date");
            });

            modelBuilder.Entity<QuestionNo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("QuestionNo");

                entity.Property(e => e.Mark)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.QuestionNo1).HasColumnName("QuestionNo");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany()
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionNo_QuestionDetail");

                entity.HasOne(d => d.QuestionNo1Navigation)
                    .WithMany()
                    .HasForeignKey(d => d.QuestionNo1)
                    .HasConstraintName("FK_QuestionNo_TestCaseDb");
            });

            modelBuilder.Entity<TestCaseDb>(entity =>
            {
                entity.HasKey(e => e.QuestionNo)
                    .HasName("PK_TestCase");

                entity.ToTable("TestCaseDb");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.DateCreate).HasColumnType("date");

                entity.Property(e => e.DateUpdate).HasColumnType("date");

                entity.Property(e => e.Mssv)
                    .IsUnicode(false)
                    .HasColumnName("MSSV");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.PassWord).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Class");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
