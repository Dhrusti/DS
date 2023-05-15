using BussinessLayer;
using Helper;
using ServiceLayer.Implementation;
using ServiceLayer.Interface;
using System.Collections.Generic;

namespace Arche_CSC_WebApi
{
	public static class ServiceExtension
	{
		public static void DIScopes(this IServiceCollection services)
		{
			//Helpers
			services.AddScoped<CommonRepo>();
			services.AddScoped<CommonHelper>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//BLL
			services.AddScoped<ArcheCSCBLL>();

			//Services
			services.AddScoped<IArcheCSC, ArcheCSCImpl>();
		}
	}
}
