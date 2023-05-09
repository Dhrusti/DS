using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Entities
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<docimg_document> docimg_documents { get; set; }
        public virtual DbSet<docimg_storage> docimg_storages { get; set; }
        public virtual DbSet<filemst> filemsts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=portalwebapi_db;uid=root;pwd=sa@123", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<docimg_document>(entity =>
            {
                entity.HasKey(e => e.ipkdoc_id)
                    .HasName("PRIMARY");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.ifkstorage_id, "FK_STORAGE_ID_idx");

                entity.HasIndex(e => e.ifkAccountID, "IDX_ACC_ID");

                entity.HasIndex(e => e.sDoc_type, "IDX_DOC_TYPE");

                entity.HasIndex(e => e.iProcessed, "IDX_IRPOCESS");

                entity.HasIndex(e => e.sRef_number, "IDX_REF_NUM");

                entity.Property(e => e.dDate_entered).HasColumnType("timestamp");

                entity.Property(e => e.iProcessed).HasDefaultValueSql("'0'");

                entity.Property(e => e.ifkAccountID)
                    .HasDefaultValueSql("'-1'")
                    .HasComment("IF the refernce number is for a account , the accounts id will be placed here, or the accountid directly can be placed here if needed");

                entity.Property(e => e.sDoc_description).HasMaxLength(50);

                entity.Property(e => e.sDoc_type).HasMaxLength(45);

                entity.Property(e => e.sFile_ext).HasMaxLength(10);

                entity.Property(e => e.sFilename)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.sInternal_filename).HasMaxLength(150);

                entity.Property(e => e.sRef_number).HasMaxLength(15);

                entity.HasOne(d => d.ifkstorage)
                    .WithMany(p => p.docimg_documents)
                    .HasForeignKey(d => d.ifkstorage_id)
                    .HasConstraintName("FK_STORAGE_ID");
            });

            modelBuilder.Entity<docimg_storage>(entity =>
            {
                entity.HasKey(e => e.ipkstorage_id)
                    .HasName("PRIMARY");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.dDate_entered).HasColumnType("timestamp");

                entity.Property(e => e.iEnabled)
                    .HasDefaultValueSql("'1'")
                    .HasComment("Is storage still available to use");

                entity.Property(e => e.sLinuxLocationPath).HasMaxLength(255);

                entity.Property(e => e.sStorage_location)
                    .HasMaxLength(255)
                    .HasComment("Location on storage device where files are stored");

                entity.Property(e => e.sStorage_name)
                    .HasMaxLength(50)
                    .HasComment("Name given to the storage to allow easy selection at config or storage times");

                entity.Property(e => e.sStorage_server_name)
                    .HasMaxLength(150)
                    .HasComment("Can be IP address or hostname");

                entity.Property(e => e.sStorage_server_password)
                    .HasMaxLength(20)
                    .HasComment("Password for storage server IF required");

                entity.Property(e => e.sStorage_server_username)
                    .HasMaxLength(20)
                    .HasComment("Username for storage server IF required");

                entity.Property(e => e.sStorage_sub_type).HasMaxLength(10);

                entity.Property(e => e.sStorage_type).HasMaxLength(10);
            });

            modelBuilder.Entity<filemst>(entity =>
            {
                entity.ToTable("filemst");

                entity.Property(e => e.ClientIP)
                    .HasMaxLength(50)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.FileFormat).HasMaxLength(100);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.FilePath).HasMaxLength(500);

                entity.Property(e => e.FileSize).HasMaxLength(100);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
