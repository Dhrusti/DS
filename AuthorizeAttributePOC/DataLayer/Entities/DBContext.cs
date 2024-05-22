using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLogMst> ActivityLogMsts { get; set; }

    public virtual DbSet<ExceptionLogMst> ExceptionLogMsts { get; set; }

    public virtual DbSet<UserMst> UserMsts { get; set; }

    public virtual DbSet<UserStatusMst> UserStatusMsts { get; set; }

    public virtual DbSet<UserTokenMst> UserTokenMsts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;user=sa;password=123;Database=AuthorizeAttributeDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLogMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3214EC07434940D4");

            entity.ToTable("ActivityLogMst");

            entity.Property(e => e.Apiurl).HasColumnName("APIURL");
            entity.Property(e => e.ExecutionDate).HasColumnType("datetime");
            entity.Property(e => e.MethodType).HasMaxLength(10);
        });

        modelBuilder.Entity<ExceptionLogMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exceptio__3214EC07EE6B972D");

            entity.ToTable("ExceptionLogMst");

            entity.Property(e => e.Apiurl).HasColumnName("APIURL");
            entity.Property(e => e.ExecutionDate).HasColumnType("datetime");
            entity.Property(e => e.MethodType).HasMaxLength(10);
        });

        modelBuilder.Entity<UserMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserMst__3214EC07A851398F");

            entity.ToTable("UserMst");

            entity.Property(e => e.ContactNo).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserType).HasMaxLength(100);
        });

        modelBuilder.Entity<UserStatusMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserStat__3214EC07A80F7F62");

            entity.ToTable("UserStatusMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserStatus).HasMaxLength(20);
        });

        modelBuilder.Entity<UserTokenMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserToke__3214EC07E5184E88");

            entity.ToTable("UserTokenMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.RefreshTokenExpiryTime).HasColumnType("datetime");
            entity.Property(e => e.TokenExpiryTime).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
