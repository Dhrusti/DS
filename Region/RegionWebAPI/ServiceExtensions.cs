using BusinessLayer;
using ServiceLayer.Implementation;
using ServiceLayer.Interface;

namespace RegionWebAPI
{
    public static class ServiceExtensions
    {
        public static void DIScopes(this IServiceCollection services)
        {
            services.AddScoped<CountryBLL>();
            services.AddScoped<ICountry, ImplCountry>();

            services.AddScoped<StateBLL>();
            services.AddScoped<IState, ImplState>();

            services.AddScoped<CityBLL>();
            services.AddScoped<ICity, ImplCity>();

        }
    }
}
