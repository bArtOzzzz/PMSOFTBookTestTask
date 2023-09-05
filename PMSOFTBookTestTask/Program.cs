using PMSOFTBookTestTask.Repository.Abstract;
using PMSOFTBookTestTask.Repository.Context;
using PMSOFTBookTestTask.Service.Abstract;
using PMSOFTBookTestTask.Models.Request;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PMSOFTBookTestTask.Repository;
using PMSOFTBookTestTask.Validation;
using FluentValidation.AspNetCore;
using PMSOFTBookTestTask.Service;
using PMSOFTBookTestTask.Mapper;
using FluentValidation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// AutoMapper settings
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IValidator<BookModel>, BookValidator>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Serilog setup
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/arquivo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Connection to the database
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"), b => b.MigrationsAssembly("PMSOFTBookTestTask.Repository"));
});

// Add Fluent Validation
builder.Services.AddFluentValidationClientsideAdapters();

// CORS policy settings
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", config =>
    {
        config.SetIsOriginAllowedToAllowWildcardSubdomains()
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .Build();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
