using AspWebApi.Controllers;
using AspWebApi.Validations;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Managers;
using DataAccessLayer.Interface;
using DataAccessLayer.Services;
using FluentValidation.AspNetCore;
using Models;
using System;
public class Program
{
    public static void Main(string[] args)
    {
        

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddTransient<IEmployeeManager, EmployeeManager>();
        builder.Services.AddTransient<IRoleManager, RoleManager>();

        builder.Services.AddTransient<IDepartmentManager, DepartmentManager>();
        builder.Services.AddTransient<ILocationManager, LocationManager>();

        builder.Services.AddTransient<IProjectManager, ProjectManager>();

        builder.Services.AddTransient<IDataOperations, DataOperations>();
        builder.Services.AddControllers();
        builder.Services.AddControllers().AddFluentValidation(fv => {
            fv.RegisterValidatorsFromAssemblyContaining<Employee>();
        });        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
    }
}
