using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TorcAssestmentAPI.Models;
using TorcAssestmentAPI.Repository;
using TorcAssestmentAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbTorcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sql => sql.EnableRetryOnFailure(5)));

// AutoMapper
builder.Services.AddAutoMapper(typeof(EmployeeProfile).Assembly);

// FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<EmployeeDtoValidator>();
    });

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
