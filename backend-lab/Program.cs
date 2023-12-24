using backend_lab;
using backend_lab.Services;
using DataLayer;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<CategoryService>();
builder.Services.AddTransient<RecordService>();
builder.Services.AddTransient<AccountService>();
builder.Services.AddTransient<RepoPull>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");
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

app.MapControllers();

app.Run();
