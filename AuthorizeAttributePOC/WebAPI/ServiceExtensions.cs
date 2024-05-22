﻿using BussinessLayer;
using Helper.CommonHelpers;
using ServiceLayer;

namespace WebAPI
{
    public static class ServiceExtensions
    {
        public static void DIScopes(this IServiceCollection services)
        {
            //Helper
            services.AddScoped<CommonHelper>();
            services.AddScoped<CommonRepo>();
            services.AddScoped<AuthHelper>();

            //BLL
            services.AddScoped<AuthBLL>();
            services.AddScoped<UserBLL>();

            //Services
            services.AddScoped<IAuth, AuthImpl>();
            services.AddScoped<IUser, UserImpl>();
        }
    }
}