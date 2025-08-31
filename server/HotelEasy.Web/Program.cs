using dotenv.net;
using Microsoft.EntityFrameworkCore;
using HotelEasy.Data;
using HotelEasy.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HotelEasy.Services.Mapping;
using CloudinaryDotNet;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
var JWTKey = Environment.GetEnvironmentVariable("JWTKey")
             ?? throw new InvalidOperationException("JWTKey env is missing!");
var JWTIssuer = Environment.GetEnvironmentVariable("JWTIssuer")
             ?? throw new InvalidOperationException("JWTIssuer env is missing!");
var JWTAudience = Environment.GetEnvironmentVariable("JWTAudience")
             ?? throw new InvalidOperationException("JWTAudience env is missing!");

builder.Services.AddDbContext<Diplomna21180105Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(Environment.GetEnvironmentVariable("CLIENT_URL"))
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelEasy API", Version = "v1" });
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put ONLY your JWT Bearer token here:",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme,
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JWTIssuer,
        ValidAudience = JWTAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKey))
    };
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);
var cloudinary = new Cloudinary(new CloudinaryDotNet.Account(
    Environment.GetEnvironmentVariable("CloudName"),
    Environment.GetEnvironmentVariable("ApiKey"),
    Environment.GetEnvironmentVariable("ApiSecret")
));
builder.Services.AddSingleton(cloudinary);

StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");

// Register services
builder.Services.AddScoped<AuthServices>();
builder.Services.AddScoped<HotelsServices>();
builder.Services.AddScoped<RoomsServices>();
builder.Services.AddScoped<ReservationServices>();
builder.Services.AddScoped<CloudinaryImageService>();
builder.Services.AddScoped<PaymentsServices>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.UseCors("AllowSpecificOrigins");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
