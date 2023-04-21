using BussinessLayer;
using Helpers.CommonHelpers;
using ServiceLayer.Implementation;
using ServiceLayer.Interface;

namespace WebAPI
{
    public static class ServiceExtensions
    {
        public static void DIScopes(this IServiceCollection services)
        {
            //Helpers
            services.AddScoped<CommonHelper>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //BLL
            services.AddScoped<AuthBLL>();
            services.AddScoped<LevelBLL>();
            services.AddScoped<VoucherNameBLL>();
            services.AddScoped<CostCenterBLL>();
            services.AddScoped<RefereceDocumentCategoryBLL>();

            //Services
            services.AddScoped<IAuth, AuthImpl>();
            services.AddScoped<ILevels, LevelsImpl>();
            services.AddScoped<IVoucherName, VoucherNameImpl>();
            services.AddScoped<ICostCenter, CostCenterImpl>();
            services.AddScoped<ICostCenter, CostCenterImpl>();
            services.AddScoped<IRefereceDocumentCategory, RefereceDocumentCategoryImpl>();
        }
    }
}
