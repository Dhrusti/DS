﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer
{
    public partial class HIMSAuthenticationDBContext : DbContext
    {
        public HIMSAuthenticationDBContext()
        {
        }

        public HIMSAuthenticationDBContext(DbContextOptions<HIMSAuthenticationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TokenMst> TokenMsts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.;user=sa;password=123;Database=HIMSAuthenticationDB;TrustServerCertificate=True");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TokenMst>(entity =>
            {
                entity.HasKey(e => e.TokenId)
                    .HasName("PK__TokenMst__658FEEEA0377343D");

                entity.ToTable("TokenMst");

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.ExpiredOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
