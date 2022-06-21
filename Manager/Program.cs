using FluentValidation;
using Manager.Context.Data;
using Manager.Context.Repositorio;
using Manager.Context.Repositorio.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependence for MediatR
var assembly = Assembly.Load("Manager.Application");
builder.Services.AddMediatR(assembly);

//Depende for validator
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Manager.Application"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepository<T>, Repository<T>>();

string strConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(strConnection, sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); });
     
    
}, ServiceLifetime.Scoped);



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
