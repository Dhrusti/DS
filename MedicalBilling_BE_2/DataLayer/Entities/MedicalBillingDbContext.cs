using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities;

public partial class MedicalBillingDbContext : DbContext
{
    public MedicalBillingDbContext()
    {
    }

    public MedicalBillingDbContext(DbContextOptions<MedicalBillingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActionStatusMst> ActionStatusMsts { get; set; }

    public virtual DbSet<ActivityLogMst> ActivityLogMsts { get; set; }

    public virtual DbSet<AgingMst> AgingMsts { get; set; }

    public virtual DbSet<AppointmentMst> AppointmentMsts { get; set; }

    public virtual DbSet<CallTypeMst> CallTypeMsts { get; set; }

    public virtual DbSet<ClaimMst> ClaimMsts { get; set; }

    public virtual DbSet<ClaimStatusMst> ClaimStatusMsts { get; set; }

    public virtual DbSet<ClientMst> ClientMsts { get; set; }

    public virtual DbSet<CompanyMst> CompanyMsts { get; set; }

    public virtual DbSet<DefaultPermission> DefaultPermissions { get; set; }

    public virtual DbSet<DepartmentMst> DepartmentMsts { get; set; }

    public virtual DbSet<DurationMst> DurationMsts { get; set; }

    public virtual DbSet<ExtensionMst> ExtensionMsts { get; set; }

    public virtual DbSet<FileCategoryHistoryMst> FileCategoryHistoryMsts { get; set; }

    public virtual DbSet<FileDataMst> FileDataMsts { get; set; }

    public virtual DbSet<FileHistoryMst> FileHistoryMsts { get; set; }

    public virtual DbSet<LinkMst> LinkMsts { get; set; }

    public virtual DbSet<NotificationMst> NotificationMsts { get; set; }

    public virtual DbSet<OrganizationMst> OrganizationMsts { get; set; }

    public virtual DbSet<PatientEmailMst> PatientEmailMsts { get; set; }

    public virtual DbSet<PatientMst> PatientMsts { get; set; }

    public virtual DbSet<PayerMst> PayerMsts { get; set; }

    public virtual DbSet<PermissionMst> PermissionMsts { get; set; }

    public virtual DbSet<PhysicianMst> PhysicianMsts { get; set; }

    public virtual DbSet<PolicyMst> PolicyMsts { get; set; }

    public virtual DbSet<RemarkMst> RemarkMsts { get; set; }

    public virtual DbSet<RoleMst> RoleMsts { get; set; }

    public virtual DbSet<ServiceMst> ServiceMsts { get; set; }

    public virtual DbSet<UserMst> UserMsts { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    public virtual DbSet<UserTokenMst> UserTokenMsts { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;user=sa;password=123;Database=MedicalBillingDB;Encrypt=False;Trusted_Connection=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActionStatusMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ActionSt__3214EC0798A9A366");

            entity.ToTable("ActionStatusMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.StatusName).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ActivityLogMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3214EC27590D2433");

            entity.ToTable("ActivityLogMst");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apiurl).HasColumnName("APIURL");
            entity.Property(e => e.ExecutionDate).HasColumnType("datetime");
            entity.Property(e => e.MethodType).HasMaxLength(10);
        });

        modelBuilder.Entity<AgingMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AgingMst__3214EC07556F115C");

            entity.ToTable("AgingMst");

            entity.Property(e => e.ChargeAmount).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.ClaimCode).HasMaxLength(50);
            entity.Property(e => e.ClaimStatus).HasMaxLength(50);
            entity.Property(e => e.Cob)
                .HasMaxLength(50)
                .HasColumnName("COB");
            entity.Property(e => e.Componant).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfService).HasColumnType("datetime");
            entity.Property(e => e.DiagnosisCode1).HasMaxLength(50);
            entity.Property(e => e.DiagnosisCode2).HasMaxLength(50);
            entity.Property(e => e.DiagnosisCode3).HasMaxLength(50);
            entity.Property(e => e.DiagnosisCode4).HasMaxLength(50);
            entity.Property(e => e.Eaibcode)
                .HasMaxLength(50)
                .HasColumnName("EAIBCode");
            entity.Property(e => e.InsuranceAmount1).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.InsuranceAmount2).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.InsuranceAmount3).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.InsuranceAmount4).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.LastBillDate).HasColumnType("datetime");
            entity.Property(e => e.LineItemAmount).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.MedicalRecordCode).HasMaxLength(50);
            entity.Property(e => e.Modifier).HasMaxLength(100);
            entity.Property(e => e.PatientCode).HasMaxLength(50);
            entity.Property(e => e.PatientDob)
                .HasColumnType("datetime")
                .HasColumnName("PatientDOB");
            entity.Property(e => e.PatientName).HasMaxLength(100);
            entity.Property(e => e.PayerCode).HasMaxLength(50);
            entity.Property(e => e.PayerName).HasMaxLength(100);
            entity.Property(e => e.PayerPhone).HasMaxLength(50);
            entity.Property(e => e.PolicyCode).HasMaxLength(50);
            entity.Property(e => e.RefferingFullName).HasMaxLength(100);
            entity.Property(e => e.RenderingFullName).HasMaxLength(100);
            entity.Property(e => e.ServiceCode).HasMaxLength(50);
            entity.Property(e => e.ServiceCpt)
                .HasMaxLength(50)
                .HasColumnName("ServiceCPT");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<AppointmentMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC07EC79EDF6");

            entity.ToTable("AppointmentMst");

            entity.Property(e => e.AccountNo).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ActualAppoitmentDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DoctorGender).HasMaxLength(20);
            entity.Property(e => e.IdExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.IsAppoitmentVehicleOrworkInjury).HasDefaultValueSql("((1))");
            entity.Property(e => e.IsCovidPossitive).HasDefaultValueSql("((1))");
            entity.Property(e => e.IsIdCurrentOrExpired).HasMaxLength(50);
            entity.Property(e => e.IsMatchInsurance)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.IsVaccinated)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.LastAppoitmentDate).HasColumnType("datetime");
            entity.Property(e => e.NewAppoitmentDate).HasColumnType("datetime");
            entity.Property(e => e.PatientDob).HasColumnName("PatientDOB");
            entity.Property(e => e.Pcp)
                .HasMaxLength(50)
                .HasColumnName("PCP");
            entity.Property(e => e.PcpmobileNo)
                .HasMaxLength(20)
                .HasColumnName("PCPMobileNo");
            entity.Property(e => e.PrimaryInsuranceId).HasMaxLength(50);
            entity.Property(e => e.PrimaryInsuranceName).HasMaxLength(50);
            entity.Property(e => e.ReferingMd)
                .HasMaxLength(50)
                .HasColumnName("ReferingMD");
            entity.Property(e => e.ReferingMobileNo).HasMaxLength(20);
            entity.Property(e => e.SecondaryInsuranceId).HasMaxLength(50);
            entity.Property(e => e.SecondaryInsuranceName).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TaxId).HasMaxLength(20);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CallTypeMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CallType__3214EC07F771ABEE");

            entity.ToTable("CallTypeMst");

            entity.Property(e => e.CallTypeName).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ClaimMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ClaimMst__3214EC076C16F48D");

            entity.ToTable("ClaimMst");

            entity.Property(e => e.ClaimCode).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.LastBillDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ClaimStatusMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ClaimSta__3214EC078247D169");

            entity.ToTable("ClaimStatusMst");

            entity.Property(e => e.ClaimStatusName).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ClientMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ClientMs__3214EC074C22B101");

            entity.ToTable("ClientMst");

            entity.Property(e => e.AppoitmentEmail).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DoctorEmail).HasMaxLength(50);
            entity.Property(e => e.FaxNo).HasMaxLength(20);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.HomeName).HasMaxLength(100);
            entity.Property(e => e.InfoEmail).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.MobileNo).HasMaxLength(20);
            entity.Property(e => e.OfficeName).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.StreetName).HasMaxLength(100);
            entity.Property(e => e.StreetNo).HasMaxLength(50);
            entity.Property(e => e.Suburb).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CompanyMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompanyM__3214EC078D1B76CA");

            entity.ToTable("CompanyMst");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Bcn)
                .HasMaxLength(50)
                .HasColumnName("BCN");
            entity.Property(e => e.CompanyDisplayName).HasMaxLength(50);
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FaxNo).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(15);
            entity.Property(e => e.Npi)
                .HasMaxLength(50)
                .HasColumnName("NPI");
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Sonarx).HasMaxLength(50);
            entity.Property(e => e.TaxId).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Website).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(15);
        });

        modelBuilder.Entity<DefaultPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DefaultP__3214EC07045987F5");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DepartmentMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC077110321F");

            entity.ToTable("DepartmentMst");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Bcn)
                .HasMaxLength(50)
                .HasColumnName("BCN");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DepartmentDisplayName).HasMaxLength(50);
            entity.Property(e => e.DepartmentName).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FaxNo).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(15);
            entity.Property(e => e.Npi)
                .HasMaxLength(50)
                .HasColumnName("NPI");
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Sonarx).HasMaxLength(50);
            entity.Property(e => e.TaxId).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Website).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(15);
        });

        modelBuilder.Entity<DurationMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Duration__3214EC070024A2E7");

            entity.ToTable("DurationMst");

            entity.Property(e => e.AppointmentId).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ExtensionMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Extensio__3214EC07B91D1554");

            entity.ToTable("ExtensionMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExtensionName).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<FileCategoryHistoryMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FileCate__3214EC0757B10311");

            entity.ToTable("FileCategoryHistoryMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FileCategoryName).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<FileDataMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FileData__3214EC07CBEDC4A5");

            entity.ToTable("FileDataMst");

            entity.Property(e => e.ChargeAmount).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.ClaimCode).HasMaxLength(50);
            entity.Property(e => e.ClaimStatus).HasMaxLength(50);
            entity.Property(e => e.Cob)
                .HasMaxLength(50)
                .HasColumnName("COB");
            entity.Property(e => e.Componant).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfService).HasColumnType("datetime");
            entity.Property(e => e.DiagnosisCode1).HasMaxLength(50);
            entity.Property(e => e.DiagnosisCode2).HasMaxLength(50);
            entity.Property(e => e.DiagnosisCode3).HasMaxLength(50);
            entity.Property(e => e.DiagnosisCode4).HasMaxLength(50);
            entity.Property(e => e.Eaibcode)
                .HasMaxLength(50)
                .HasColumnName("EAIBCode");
            entity.Property(e => e.InsuranceAmount1).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.InsuranceAmount2).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.InsuranceAmount3).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.InsuranceAmount4).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.LastBillDate).HasColumnType("datetime");
            entity.Property(e => e.LineItemAmount).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.MedicalRecordCode).HasMaxLength(50);
            entity.Property(e => e.Modifier).HasMaxLength(100);
            entity.Property(e => e.PatientCode).HasMaxLength(50);
            entity.Property(e => e.PatientDob)
                .HasColumnType("datetime")
                .HasColumnName("PatientDOB");
            entity.Property(e => e.PatientName).HasMaxLength(100);
            entity.Property(e => e.PayerCode).HasMaxLength(50);
            entity.Property(e => e.PayerName).HasMaxLength(100);
            entity.Property(e => e.PayerPhone).HasMaxLength(50);
            entity.Property(e => e.PolicyCode).HasMaxLength(50);
            entity.Property(e => e.RefferingFullName).HasMaxLength(100);
            entity.Property(e => e.RenderingFullName).HasMaxLength(100);
            entity.Property(e => e.ServiceCode).HasMaxLength(50);
            entity.Property(e => e.ServiceCpt)
                .HasMaxLength(50)
                .HasColumnName("ServiceCPT");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<FileHistoryMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FileHist__3214EC07C3B19E4F");

            entity.ToTable("FileHistoryMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.FileExtension).HasMaxLength(10);
            entity.Property(e => e.FileName).HasMaxLength(100);
            entity.Property(e => e.FileSize).HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<LinkMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LinkMst__3214EC074C8DC8A5");

            entity.ToTable("LinkMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<NotificationMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07CAD262C2");

            entity.ToTable("NotificationMst");

            entity.Property(e => e.AdminDescriptionTitle).HasMaxLength(50);
            entity.Property(e => e.ApprovalStatus).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DescriptionTitle).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<OrganizationMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organiza__3214EC07738B9AC5");

            entity.ToTable("OrganizationMst");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Bcn)
                .HasMaxLength(50)
                .HasColumnName("BCN");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FaxNo).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(15);
            entity.Property(e => e.Npi)
                .HasMaxLength(50)
                .HasColumnName("NPI");
            entity.Property(e => e.OrganizationDisplayName).HasMaxLength(50);
            entity.Property(e => e.OrganizationName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Sonarx).HasMaxLength(50);
            entity.Property(e => e.TaxId).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Website).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(15);
        });

        modelBuilder.Entity<PatientEmailMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PatientE__3214EC072C9CF290");

            entity.ToTable("PatientEmailMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailFor).HasMaxLength(250);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.PatientEmail)
                .HasMaxLength(50)
                .HasColumnName("PatientEMail");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PatientM__3214EC071A6AD56D");

            entity.ToTable("PatientMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Eaibcode)
                .HasMaxLength(50)
                .HasColumnName("EAIBCode");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.MedicalRecordCode).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(50);
            entity.Property(e => e.PatientCode).HasMaxLength(50);
            entity.Property(e => e.PatientDob)
                .HasColumnType("datetime")
                .HasColumnName("PatientDOB");
            entity.Property(e => e.PatientName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RefferingFullName).HasMaxLength(100);
            entity.Property(e => e.RenderingFullName).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PayerMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PayerMst__3214EC073EE6BCD8");

            entity.ToTable("PayerMst");

            entity.Property(e => e.Componant).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(50);
            entity.Property(e => e.PayerCode).HasMaxLength(50);
            entity.Property(e => e.PayerName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Website).HasMaxLength(50);
        });

        modelBuilder.Entity<PermissionMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC07AFE5C7AA");

            entity.ToTable("PermissionMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.PermissionCode).HasMaxLength(50);
            entity.Property(e => e.PermissionName).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PhysicianMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Physicia__3214EC07233C851E");

            entity.ToTable("PhysicianMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DoctorDegreeName1).HasMaxLength(20);
            entity.Property(e => e.DoctorDegreeName2).HasMaxLength(20);
            entity.Property(e => e.DoctorDegreeName3).HasMaxLength(20);
            entity.Property(e => e.DoctorEmail).HasMaxLength(50);
            entity.Property(e => e.DoctorFirstName).HasMaxLength(20);
            entity.Property(e => e.DoctorLastName).HasMaxLength(20);
            entity.Property(e => e.DoctorMobileNo).HasMaxLength(20);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.SecretaryFirstName)
                .HasMaxLength(20)
                .HasColumnName("secretaryFirstName");
            entity.Property(e => e.SecretaryLastName)
                .HasMaxLength(20)
                .HasColumnName("secretaryLastName");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PolicyMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PolicyMs__3214EC079769BF94");

            entity.ToTable("PolicyMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PolicyCode).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<RemarkMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RemarkMs__3214EC07779BA02F");

            entity.ToTable("RemarkMst");

            entity.Property(e => e.AppointmentId).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<RoleMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleMst__3214EC07FEF083B3");

            entity.ToTable("RoleMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ServiceMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ServiceM__3214EC0743A76D8D");

            entity.ToTable("ServiceMst");

            entity.Property(e => e.ChargeAmount).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.Cob)
                .HasMaxLength(50)
                .HasColumnName("COB");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfService).HasColumnType("datetime");
            entity.Property(e => e.DiagnosisCode1).HasMaxLength(50);
            entity.Property(e => e.DiagnosisCode2).HasMaxLength(50);
            entity.Property(e => e.DiagnosisCode3).HasMaxLength(50);
            entity.Property(e => e.DiagnosisCode4).HasMaxLength(50);
            entity.Property(e => e.InsuranceAmount1).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.InsuranceAmount2).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.InsuranceAmount3).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.InsuranceAmount4).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.LineItemAmount).HasColumnType("decimal(38, 8)");
            entity.Property(e => e.Modifier).HasMaxLength(100);
            entity.Property(e => e.ServiceCode).HasMaxLength(50);
            entity.Property(e => e.ServiceCpt)
                .HasMaxLength(50)
                .HasColumnName("ServiceCPT");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserMst__3214EC07F7CFC021");

            entity.ToTable("UserMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.MobileNo).HasMaxLength(20);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPerm__3214EC07D8C96825");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserTokenMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserToke__3214EC07CADF563D");

            entity.ToTable("UserTokenMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiredOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
