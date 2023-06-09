using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ValidationDemoApi;
using ValidationDemoApi.Entities;
using ValidationDemoApi.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<Validate>();
builder.Services.AddScoped<GenerateToken>();
builder.Services.AddScoped<SendSMSOTP>();
builder.Services.AddScoped<DESEncDec>();
builder.Services.AddScoped<FileDownloadandUpload>();
builder.Services.AddScoped<NumberToWords>();
builder.Services.AddScoped<ExportToExcelData>();
builder.Services.AddScoped<LinqAllListMethod>();
builder.Services.AddScoped<DateTemp>();
builder.Services.AddScoped<Currencyconvertdata>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:KeyToken").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<JWTokenDBContext>(
  options => options.UseSqlServer(builder.Configuration.GetConnectionString("JWTTokenConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
