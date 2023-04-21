using DataLayer.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebAPI;
using WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.DIScopes();

builder.Services.AddControllers(config =>
{
    config.Filters.Add<ExceptionFilter>();
    config.Filters.Add<AuthorizationFilter>();
    config.Filters.Add<ActionFilter>();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDirectoryBrowser();

builder.Services.AddSwaggerGen();

if (Convert.ToBoolean(builder.Configuration.GetSection("CommonSwitches:AuthenticationEnable").Value))
{
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        x =>
    {
            x.SlidingExpiration = false; // if (true) will work as idea time logout. else, will expire as per expiry time.
            x.ExpireTimeSpan = new TimeSpan(0, 0, 30);
            x.LoginPath = "/api/Auth/Login"; // not necessary
            x.LogoutPath = "/api/Auth/Logout"; // not necessary
            x.Events = new CookieAuthenticationEvents // not necessary (Return 404 Code instead of 401 if not used)
        {
                OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                },
                OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
        }
            };
    });
};
builder.Services.Configure<DatabaseSettings>(
	builder.Configuration.GetSection("DatabaseConnection")
	);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI(x => x.DefaultModelsExpandDepth(-1));

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

if (Convert.ToBoolean(builder.Configuration.GetSection("CommonSwitches:AuthenticationEnable").Value))
    app.MapControllers().RequireAuthorization();
else
    app.MapControllers();

app.Run();