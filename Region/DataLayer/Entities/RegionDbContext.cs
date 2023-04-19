using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer.Entities
{
    public partial class RegionDbContext : DbContext
    {
        public RegionDbContext()
        {
        }

        public RegionDbContext(DbContextOptions<RegionDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CityMst> CityMsts { get; set; } = null!;
        public virtual DbSet<CountryMst> CountryMsts { get; set; } = null!;
        public virtual DbSet<StateMst> StateMsts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //                optionsBuilder.UseSqlServer("Server=ARCHE-ITD450\\SQLEXPRESS;user=sa;password=123;Database=RegionDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityMst>(entity =>
            {
                entity.HasKey(e => e.CityMainId)
                    .HasName("PK__CityMst__32D2A968D5DB2BBF");

                entity.ToTable("CityMst");

                entity.Property(e => e.CityName).HasMaxLength(250);

                entity.Property(e => e.Latitude).HasMaxLength(250);

                entity.Property(e => e.Longitude).HasMaxLength(250);
            });

            modelBuilder.Entity<CountryMst>(entity =>
            {
                entity.HasKey(e => e.CountryMainId)
                    .HasName("PK__CountryM__31F08912E91A1611");

                entity.ToTable("CountryMst");

                entity.Property(e => e.Capital).HasMaxLength(250);

                entity.Property(e => e.CountryName).HasMaxLength(250);

                entity.Property(e => e.Currency).HasMaxLength(250);

                entity.Property(e => e.CurrencyName).HasMaxLength(250);

                entity.Property(e => e.CurrencySymbol).HasMaxLength(250);

                entity.Property(e => e.Emoji).HasMaxLength(250);

                entity.Property(e => e.EmojiU).HasMaxLength(250);

                entity.Property(e => e.Iso2).HasMaxLength(250);

                entity.Property(e => e.Iso3).HasMaxLength(250);

                entity.Property(e => e.Latitude).HasMaxLength(250);

                entity.Property(e => e.Longitude).HasMaxLength(250);

                entity.Property(e => e.Native).HasMaxLength(250);

                entity.Property(e => e.NumericCode).HasMaxLength(250);

                entity.Property(e => e.PhoneCode).HasMaxLength(250);

                entity.Property(e => e.Region).HasMaxLength(250);

                entity.Property(e => e.SubRegion).HasMaxLength(250);

                entity.Property(e => e.Tld).HasMaxLength(250);
            });

            modelBuilder.Entity<StateMst>(entity =>
            {
                entity.HasKey(e => e.StateMainId)
                    .HasName("PK__StateMst__35D80E037E5FECB6");

                entity.ToTable("StateMst");

                entity.Property(e => e.Latitude).HasMaxLength(250);

                entity.Property(e => e.Longitude).HasMaxLength(250);

                entity.Property(e => e.StateCode).HasMaxLength(250);

                entity.Property(e => e.StateName).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
