using AspNetCoreRateLimit;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PortalWebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PortalWebAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.DIScopes();

			services.AddDbContext<DBContext>(x =>
			{
				x.UseMySQL(Configuration.GetConnectionString("DBConnection"));
			});

			services.AddMemoryCache();

			services.Configure<IpRateLimitOptions>(x =>
			{
				x.EnableEndpointRateLimiting = Convert.ToBoolean(Configuration.GetSection("EnableEndpointRateLimiting").Value);
				x.StackBlockedRequests = Convert.ToBoolean(Configuration.GetSection("StackBlockedRequests").Value);
				x.HttpStatusCode = 429;
				x.RealIpHeader = "X-Real-IP";
				x.ClientIdHeader = "X-ClientId";
				x.GeneralRules = new List<RateLimitRule>
				{
					new RateLimitRule
					{
                        //Endpoint="*",
                        Endpoint = Configuration.GetSection("Endpoint").Value,
						Period = Configuration.GetSection("Period").Value,
						Limit = Convert.ToInt16(Configuration.GetSection("Limit").Value)
					}
				};
			});

			services.AddInMemoryRateLimiting();

			services.AddControllers(x =>
			{
				x.Filters.Add<ExceptionFilter>();
				x.Filters.Add<AuthorizationFilter>();
				x.Filters.Add<ActionFilter>();
			});

			//services.AddSwaggerGen(x =>
			//{
			//    x.SwaggerDoc("v1", new OpenApiInfo { Title = "PortalWebAPI", Version = "v1" });
			//});

			//services.AddSwaggerGen(options =>
			//{
			//	options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			//	{
			//		Name = "Authorization",
			//		Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
			//		Scheme = "Bearer",
			//		BearerFormat = "JWT",
			//		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
			//		Description = "JWT Authorization header using the Bearer scheme."
			//	});
			//	options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
			//	{
			//		new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			//		{
			//			Reference = new Microsoft.OpenApi.Models.OpenApiReference
			//			{
			//				Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
			//				Id = "Bearer"
			//			}
			//		}
			//	});
			//	new string[] {}
			//});


			// Add Authorization Option In swagger
			services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = Microsoft.OpenApi.Models.ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme."
				});
				options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
				{
			new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
					Reference = new Microsoft.OpenApi.Models.OpenApiReference {
						Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
							Id = "Bearer"
					}
				},
				new string[] {}
				}
			});
			});

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					var Key = Encoding.UTF8.GetBytes(Configuration.GetSection("JsonWebTokenKeys:IssuerSigningKey").Value);
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JsonWebTokenKeys:IssuerSigningKey").Value)),
						ValidIssuer = Configuration.GetSection("JsonWebTokenKeys:ValidIssuer").Value,
						ValidAudience = Configuration.GetSection("JsonWebTokenKeys:ValidAudience").Value,
						ClockSkew = TimeSpan.FromMinutes(Convert.ToInt32(Configuration.GetSection("JsonWebTokenKeys:TokenExpiryMin").Value)),
					};
					options.Events = new JwtBearerEvents
					{
						OnAuthenticationFailed = context =>
						{
							if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
							{
								context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
							}
							return Task.CompletedTask;
						}
					};
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseDeveloperExceptionPage();

			app.UseIpRateLimiting();

			app.UseSwagger();

			app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "PortalWebAPI v1"));

			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(x =>
			{
				x.MapControllers().RequireAuthorization();
			});
		}


	}
}
