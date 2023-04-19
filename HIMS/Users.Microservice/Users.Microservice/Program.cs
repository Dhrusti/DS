using BusinessLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.IService;
using ServiceLayer.ServiceImpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<HIMSUserDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("HIMSUserDBConnection")));
builder.Services.AddCors(options => options.AddDefaultPolicy(
                             builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                                 ));
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IUser, UserImpl>();
builder.Services.AddScoped<IRegion, RegionImpl>();
builder.Services.AddScoped<UserBLL>();
builder.Services.AddScoped<RegionBLL>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
