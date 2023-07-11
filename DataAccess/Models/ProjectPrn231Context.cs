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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
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
                entity.HasKey(e => new { e.StudentId, e.QuestionId });

                entity.ToTable("Question");

                entity.Property(e => e.QuestionId).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.UpdateDate).HasColumnType("date");

                entity.HasOne(d => d.QuestionNavigation)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_QuestionDetail");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mark_User");
            });

            modelBuilder.Entity<QuestionDetail>(entity =>
            {
                entity.HasKey(e => e.QuestionId);

                entity.ToTable("QuestionDetail");

                entity.Property(e => e.QuestionId).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Note).IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("date");
            });

            modelBuilder.Entity<QuestionNo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("QuestionNo");

                entity.Property(e => e.InputTestCase).IsUnicode(false);

                entity.Property(e => e.Mark)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Output).IsUnicode(false);

                entity.Property(e => e.OutputTestCase).IsUnicode(false);

                entity.Property(e => e.QuestionId).HasMaxLength(50);

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
