using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer
{
    public partial class HIMSAppointmentDBContext : DbContext
    {
        public HIMSAppointmentDBContext()
        {
        }

        public HIMSAppointmentDBContext(DbContextOptions<HIMSAppointmentDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppointmentMst> AppointmentMsts { get; set; } = null!;
        public virtual DbSet<DiagnosisMst> DiagnosisMsts { get; set; } = null!;
        public virtual DbSet<MedicineMst> MedicineMsts { get; set; } = null!;
        public virtual DbSet<PatientDiagnosisDetail> PatientDiagnosisDetails { get; set; } = null!;
        public virtual DbSet<PatientMedicationDetail> PatientMedicationDetails { get; set; } = null!;
        public virtual DbSet<PatientProblemDetail> PatientProblemDetails { get; set; } = null!;
        public virtual DbSet<ProblemMst> ProblemMsts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;user Id=sa;password=123;Database=HIMSAppointmentDB;TrustServerCertificate=True");
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentMst>(entity =>
            {
                entity.HasKey(e => e.AppointmentId)
                    .HasName("PK__Appointm__8ECDFCC2563364A6");

                entity.ToTable("AppointmentMst");

                entity.Property(e => e.AppointmentDate).HasColumnType("date");

                entity.Property(e => e.AppointmentTime).HasMaxLength(30);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<DiagnosisMst>(entity =>
            {
                entity.HasKey(e => e.DiagnosisId)
                    .HasName("PK__Diagnosi__0C54CC7384009C69");

                entity.ToTable("DiagnosisMst");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<MedicineMst>(entity =>
            {
                entity.HasKey(e => e.MedicineId)
                    .HasName("PK__Medicine__4F212890B6D5A45D");

                entity.ToTable("MedicineMst");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<PatientDiagnosisDetail>(entity =>
            {
                entity.HasKey(e => e.PatientDiagnosisId)
                    .HasName("PK__PatientD__B0473866F4AB1E98");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DiagnosisIds).HasMaxLength(100);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<PatientMedicationDetail>(entity =>
            {
                entity.HasKey(e => e.PatientMedicationId)
                    .HasName("PK__PatientM__B08C9294A1ED7E35");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Schedule).HasMaxLength(30);

                entity.Property(e => e.Time).HasMaxLength(30);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<PatientProblemDetail>(entity =>
            {
                entity.HasKey(e => e.PatientProblemId)
                    .HasName("PK__PatientP__1D511C340982822E");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ProblemIds).HasMaxLength(100);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProblemMst>(entity =>
            {
                entity.HasKey(e => e.ProblemId)
                    .HasName("PK__ProblemM__5CED528A0BFE360E");

                entity.ToTable("ProblemMst");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
