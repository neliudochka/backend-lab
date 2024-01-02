using System.Text;
using backend_lab;
using backend_lab.Services;
using DataLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var key = Environment.GetEnvironmentVariable("KEY");
Console.WriteLine(key);
builder.Services.AddAuthorization(x =>
{
    x.DefaultPolicy = new AuthorizationPolicyBuilder(
            JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key)),
    };
});

builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<CategoryService>();
builder.Services.AddTransient<RecordService>();
builder.Services.AddTransient<AccountService>();
builder.Services.AddTransient<RepoPull>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
Console.WriteLine(connectionString);

try
{
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseNpgsql(connectionString,
            sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
    });


    Console.WriteLine("Connected to db");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
    
app.MapControllers();

app.Run();
