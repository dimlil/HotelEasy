using dotenv.net;
using Microsoft.EntityFrameworkCore;
using HotelEasy.Data;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
var envVars = DotEnv.Read();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString ?? "";


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddDbContext<Diplomna21180105Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
