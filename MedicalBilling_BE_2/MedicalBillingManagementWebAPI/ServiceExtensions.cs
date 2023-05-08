using BussinessLayer;
using Helper;
using ServiceLayer.Implementation;
using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI
{
    public static class ServiceExtensions
    {
        public static void DIScopes(this IServiceCollection services)
        {
            //Helpers
            services.AddScoped<CommonRepo>();
            services.AddScoped<CommonHelper>();
            services.AddScoped<AuthRepo>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //BLL
            services.AddScoped<AuthBLL>();
            services.AddScoped<ClientBLL>();
            services.AddScoped<DoctorBLL>();
            services.AddScoped<PatientBLL>();
            services.AddScoped<CallTypeBLL>();
            services.AddScoped<ExtensionBLL>();
            services.AddScoped<DurationBLL>();
            services.AddScoped<EmailBLL>();
            services.AddScoped<PatientPDFBLL>();
            services.AddScoped<NotificationBLL>();
            services.AddScoped<AdminDashboardBLL>();
            services.AddScoped<OrganizationBLL>();
            services.AddScoped<RoleBLL>();
            services.AddScoped<PermissionBLL>();
            services.AddScoped<CompanyBLL>();
            services.AddScoped<FileUploadBLL>();
            services.AddScoped<FileCategoryHistoryBLL>();
            services.AddScoped<PayerBLL>();
            services.AddScoped<ClaimStatusBLL>();
            services.AddScoped<ClaimBLL>();
            services.AddScoped<AgingPatientBLL>();
            services.AddScoped<DepartmentBLL>();
            services.AddScoped<UserBLL>();
            services.AddScoped<FileUploadBLL>();
            services.AddScoped<AgingPatientBLL>();
            services.AddScoped<AgingPolicyBLL>();
            services.AddScoped<ClaimBLL>();
            services.AddScoped<AgingServiceBLL>();
            
            //services
            services.AddScoped<IAuth, AuthImpl>();
            services.AddScoped<IClient, ClientImpl>();
            services.AddScoped<IDoctor, DoctorImpl>();
            services.AddScoped<IPatient, PatientImpl>();
            services.AddScoped<ICallType, CallTypeImpl>();
            services.AddScoped<IExtension, ExtensionImpl>();
            services.AddScoped<IDuration, DurationImpl>();
            services.AddScoped<IEmail, EmailImpl>();
            services.AddScoped<IPatientPDF, PatientPDFImpl>();
            services.AddScoped<INotification, NotificationImpl>();
            services.AddScoped<IAdminDashboard, AdminDashboardImpl>();
            services.AddScoped<IOrganization, OrganizationImpl>();
            services.AddScoped<IRole, RoleImpl>();
            services.AddScoped<IPermissions, PermissionsImpl>();
            services.AddScoped<ICompany, CompanyImpl>();
            services.AddScoped<IUser, UserImpl>();
            services.AddScoped<IDepartment, DepartmentImpl>();
            services.AddScoped<IFileUpload, FileUploadImpl>();
            services.AddScoped<IFileCategoryHistory, FileCategoryHistoryImpl>();
            services.AddScoped<IPayer, PayerImpl>();
            services.AddScoped<IClaimStatus, ClaimStatusImpl>();
            services.AddScoped<IClaim, ClaimImpl>();
            services.AddScoped<IAgingPatient, AgingPatientImpl>();
            services.AddScoped<IFileUpload, FileUploadImpl>();
      
        }
    }
}
