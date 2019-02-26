using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BibleBlast.API.Models;
using BibleBlast.API.Helpers;

namespace BibleBlast.API.DataAccess
{
    public partial class SqlServerAppContext : DbContext
    {
        public SqlServerAppContext(DbContextOptions<SqlServerAppContext> options) : base(options) { }

        public virtual DbSet<Award> Awards { get; set; }
        public virtual DbSet<AwardQuestion> AwardQuestions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Kid> Kids { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<QuizScore> QuizScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplySingularTableNameConvention();

            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Award>(entity =>
            {
                entity.Property(e => e.AwardId)
                    .HasColumnName("AwardID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AwardQuestion>(entity =>
            {
                entity.HasKey(e => new { e.AwardId, e.QuestionId })
                    .HasName("PK_AwardQuestion");

                entity.Property(e => e.AwardId).HasColumnName("AwardID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.HasKey(e => e.FamilyId)
                    .HasName("PK_Family")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.FamilyId)
                    .HasName("FamiyIDX")
                    .ForSqlServerIsClustered();

                entity.Property(e => e.FamilyId).HasColumnName("FamilyID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DadCell)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DadName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyPhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MomCell)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MomName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NonParentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Kid>(entity =>
            {
                entity.HasKey(e => e.KidId)
                    .HasName("PK_Kid")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.KidId)
                    .HasName("KidIDX")
                    .ForSqlServerIsClustered();

                entity.Property(e => e.KidId).HasColumnName("KidID");

                entity.Property(e => e.Birthday).HasColumnType("smalldatetime");

                entity.Property(e => e.DateRegistered).HasColumnType("smalldatetime");

                entity.Property(e => e.FamilyId).HasColumnName("FamilyID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.Kid)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kid_Family");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => new { e.PaymentId, e.FamilyId })
                    .HasName("PK_Payment");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.FamilyId).HasColumnName("FamilyID");

                entity.Property(e => e.Ammount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Date).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId)
                    .HasColumnName("QuestionID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_Category");
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.HasKey(e => new { e.KidId, e.QuestionId })
                    .HasName("PK_QuestionAnswer");

                entity.Property(e => e.KidId).HasColumnName("KidID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.Date).HasColumnType("smalldatetime");

                entity.Property(e => e.SubmittedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuizScore>(entity =>
            {
                entity.HasKey(e => new { e.KidId, e.Date })
                    .HasName("PK_QuizScore");

                entity.Property(e => e.KidId).HasColumnName("KidID");

                entity.Property(e => e.Date).HasColumnType("smalldatetime");
            });
        }
    }
}
