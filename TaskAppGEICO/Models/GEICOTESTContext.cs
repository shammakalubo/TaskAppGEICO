using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TaskAppGEICO.Models
{
    public partial class GEICOTESTContext : DbContext
    {
        public GEICOTESTContext()
        {
        }

        public GEICOTESTContext(DbContextOptions<GEICOTESTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TaskPriority> TaskPriorities { get; set; }
        public virtual DbSet<TaskStatus> TaskStatuses { get; set; }
        public virtual DbSet<TaskTable> TaskTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TaskPriority>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PK_Priority");

                entity.ToTable("TaskPriority");

                entity.Property(e => e.Pid).HasColumnName("PId");

                entity.Property(e => e.Pdescription)
                    .IsUnicode(false)
                    .HasColumnName("PDescription");

                entity.Property(e => e.Ptype)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PType");
            });

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PK_Status");

                entity.ToTable("TaskStatus");

                entity.Property(e => e.Sid).HasColumnName("SId");

                entity.Property(e => e.Sdescription)
                    .IsUnicode(false)
                    .HasColumnName("SDescription");

                entity.Property(e => e.Stype)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SType");
            });

            modelBuilder.Entity<TaskTable>(entity =>
            {
                entity.HasKey(e => e.Tid)
                    .HasName("PK_Task");

                entity.ToTable("TaskTable");

                entity.Property(e => e.Tid).HasColumnName("TId");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.TdueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TDueDate");

                entity.Property(e => e.Tname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TName");

                entity.Property(e => e.TpriorityId).HasColumnName("TPriorityId");

                entity.Property(e => e.TstatusId).HasColumnName("TStatusId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
