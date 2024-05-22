using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPI;
using Helper.CommonModels;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add Connection String 
string connection = builder.Configuration["ConnectionStrings:EntitiesConnection"] ?? "";
builder.Services.AddDbContext<DBContext>(x =>
{
    x.UseSqlServer(connection); //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Allow cors
builder.Services.AddCors(x => x.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.DIScopes();

// Add services to the container.

builder.Services.AddControllers(x =>
{
    //x.Filters.Add<ExceptionFilter>();
    //x.Filters.Add<AuthorizationFilter>();
    //x.Filters.Add<ActionFilter>();
}).ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new { Field = e.Key, Error = e.Value.Errors.First().ErrorMessage })
            .ToList();
        var errorMessages = actionContext.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        var response = new CommonResponse
        {
            Status = false,
            StatusCode = HttpStatusCode.BadRequest,
            Message = string.Join(", ", errorMessages),
            Data = errors
        };

        return new BadRequestObjectResult(response);
    };
});

// Add SignalR library
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDirectoryBrowser();

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Connexcel-BE", Version = "v1" });

    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
             new OpenApiSecurityScheme
             {
                  Reference = new OpenApiReference
                  {
                    Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                  }
             },
         new string[] {}
        }
    });
    x.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JsonWebTokenKeys:IssuerSigningKey").Value);
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JsonWebTokenKeys:IssuerSigningKey").Value)),
        ValidIssuer = builder.Configuration.GetSection("JsonWebTokenKeys:ValidIssuer").Value,
        ValidAudience = builder.Configuration.GetSection("JsonWebTokenKeys:ValidAudience").Value,
        ClockSkew = TimeSpan.Zero
    };
    x.Events = new JwtBearerEvents
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

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(x => x.DefaultModelsExpandDepth(-1));

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

if (Convert.ToBoolean(builder.Configuration.GetSection("JsonWebTokenKeys:GlobalAuthentication").Value))
    app.MapControllers().RequireAuthorization();
else
    app.MapControllers();

app.Run();
