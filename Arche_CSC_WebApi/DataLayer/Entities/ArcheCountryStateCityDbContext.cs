using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities;

public partial class ArcheCountryStateCityDbContext : DbContext
{
    public ArcheCountryStateCityDbContext()
    {
    }

    public ArcheCountryStateCityDbContext(DbContextOptions<ArcheCountryStateCityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CityMst> CityMsts { get; set; }

    public virtual DbSet<CountryMst> CountryMsts { get; set; }

    public virtual DbSet<StateMst> StateMsts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ArcheCountryStateCityDb;TrustServerCertificate=True;MultipleActiveResultSets=True;User Id=sa;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CityMst>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__CityMst__F2D21B762DD786EC");

            entity.ToTable("CityMst");

            entity.Property(e => e.CityName).HasMaxLength(250);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CountryMst>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__CountryM__10D1609FC191DA8F");

            entity.ToTable("CountryMst");

            entity.Property(e => e.CountryName).HasMaxLength(250);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DialingCode).HasMaxLength(250);
            entity.Property(e => e.Iso2).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<StateMst>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__StateMst__C3BA3B3A794D351D");

            entity.ToTable("StateMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Iso2).HasMaxLength(250);
            entity.Property(e => e.StateName).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
