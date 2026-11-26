using EventRegistrationAPI_EthanGunawan.Data;
using EventRegistrationAPI_EthanGunawan.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// This is a Web API project, so use AddControllers, not AddControllersWithViews.
builder.Services.AddControllers();

// 991789737 - Ethan Gunawan
// Register AppDbContext with SQLite using the connection string in appsettings.json.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the event service with dependency injection.
builder.Services.AddScoped<IEventService, EventService>();

// Register the registration service with dependency injection.
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

// Add Swagger/OpenAPI support.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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