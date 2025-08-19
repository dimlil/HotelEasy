using dotenv.net;
using Microsoft.EntityFrameworkCore;
using HotelEasy.Data;
using HotelEasy.Services;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

builder.Services.AddDbContext<Diplomna21180105Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Register services
builder.Services.AddScoped<AuthServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
