using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer
{
    public partial class HIMSUserDBContext : DbContext
    {
        public HIMSUserDBContext()
        {
        }

        public HIMSUserDBContext(DbContextOptions<HIMSUserDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AilmentImageDetail> AilmentImageDetails { get; set; } = null!;
        public virtual DbSet<AilmentMst> AilmentMsts { get; set; } = null!;
        public virtual DbSet<DoctorDetail> DoctorDetails { get; set; } = null!;
        public virtual DbSet<PatientDetail> PatientDetails { get; set; } = null!;
        public virtual DbSet<PatientSymptomDetail> PatientSymptomDetails { get; set; } = null!;
        public virtual DbSet<RegionsMst> RegionsMsts { get; set; } = null!;
        public virtual DbSet<RoleMst> RoleMsts { get; set; } = null!;
        public virtual DbSet<SymptomMst> SymptomMsts { get; set; } = null!;
        public virtual DbSet<UserMst> UserMsts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           /* if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;user Id=sa;password=123;Database=HIMSUserDB;TrustServerCertificate=True");
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AilmentImageDetail>(entity =>
            {
                entity.HasKey(e => e.AilmentImageId)
                    .HasName("PK__AilmentI__C15A3267B2A53436");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<AilmentMst>(entity =>
            {
                entity.HasKey(e => e.AilmentId)
                    .HasName("PK__AilmentM__1D93D2FC4C3245AA");

                entity.ToTable("AilmentMst");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<DoctorDetail>(entity =>
            {
                entity.HasKey(e => e.DoctorId)
                    .HasName("PK__DoctorDe__2DC00EBF10CFEDB9");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<PatientDetail>(entity =>
            {
                entity.HasKey(e => e.PatientId)
                    .HasName("PK__PatientD__970EC366309CF8EC");

                entity.Property(e => e.AlternateMobileNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PatientCode).HasMaxLength(255);

                entity.Property(e => e.Pincode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<PatientSymptomDetail>(entity =>
            {
                entity.HasKey(e => e.PatientSymptomId)
                    .HasName("PK__PatientS__6A42463ACAC42E7B");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<RegionsMst>(entity =>
            {
                entity.ToTable("RegionsMst");

                entity.Property(e => e.CityName).HasMaxLength(100);

                entity.Property(e => e.CountryName).HasMaxLength(100);

                entity.Property(e => e.StateName).HasMaxLength(100);
            });

            modelBuilder.Entity<RoleMst>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__RoleMst__8AFACE1AFED01C0A");

                entity.ToTable("RoleMst");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<SymptomMst>(entity =>
            {
                entity.HasKey(e => e.SymptomId)
                    .HasName("PK__SymptomM__D26ED896810350FC");

                entity.ToTable("SymptomMst");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.SymptomName).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserMst>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserMst__1788CC4CBE73FDE4");

                entity.ToTable("UserMst");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
