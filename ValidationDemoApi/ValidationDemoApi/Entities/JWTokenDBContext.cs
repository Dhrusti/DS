using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ValidationDemoApi.Entities
{
    public partial class JWTokenDBContext : DbContext
    {
        public JWTokenDBContext()
        {
        }

        public JWTokenDBContext(DbContextOptions<JWTokenDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<TblOtpmst> TblOtpmsts { get; set; } = null!;
        public virtual DbSet<TblUserDocumentMst> TblUserDocumentMsts { get; set; } = null!;
        public virtual DbSet<TblUserTokenMst> TblUserTokenMsts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=JWTokenDB;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.RollNo)
                    .HasName("PK__Student__7525F924621AF92D");

                entity.ToTable("Student");

                entity.Property(e => e.RollNo).HasColumnName("ROLL_NO");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Age).HasColumnName("AGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(250)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<TblOtpmst>(entity =>
            {
                entity.HasKey(e => e.Otpid);

                entity.ToTable("TblOTPMst");

                entity.Property(e => e.Otpid).HasColumnName("OTPID");

                entity.Property(e => e.ContactNumber).HasMaxLength(25);

                entity.Property(e => e.OneTimePassword).HasMaxLength(25);

                entity.Property(e => e.Otpcreated).HasColumnName("OTPCreated");

                entity.Property(e => e.Otpexpires).HasColumnName("OTPExpires");
            });

            modelBuilder.Entity<TblUserDocumentMst>(entity =>
            {
                entity.HasKey(e => e.DocumentId)
                    .HasName("PK__TblUserD__1ABEEF0F6E6FBDEC");

                entity.ToTable("TblUserDocumentMst");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DocumentType).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.UploadDocument).HasMaxLength(255);
            });

            modelBuilder.Entity<TblUserTokenMst>(entity =>
            {
                entity.ToTable("TblUserTokenMst");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
