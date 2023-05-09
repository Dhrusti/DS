using AspNetCoreRateLimit;
using BusinessLayer;
using Helper.CommonHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Implemetation;
using ServiceLayer.Interface;

namespace PortalWebAPI
{
    public static class ServiceExtensions
    {
        public static void DIScopes(this IServiceCollection services)
        {
			//Helpers
			services.AddScoped<CommonRepo>();
			services.AddScoped<CommonHelpers>();
            services.AddScoped<AuthRepo>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            //BLL
            services.AddScoped<FilesBLL>();
            services.AddScoped<AuthBLL>();

            //Services
            services.AddScoped<IFile, FilesImpl>();
            services.AddScoped<IAuth, AuthImpl>();
        }
    }
}
