using Microsoft.EntityFrameworkCore;
using Olympics.Business;
using Olympics.Business.AutoMapper;
using Olympics.Business.Interfaces;
using Olympics.DataAccess;
using Olympics.Repository;
using Olympics.Repository.AutoMapper;
using Olympics.Repository.Interfaces;
using Olympics.WebApi.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register business
builder.Services.AddScoped<ICountryBusiness, CountryBusiness>();
builder.Services.AddScoped<IOlympicResultBusiness, OlympicResultBusiness>();

//Register repositories
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IOlympicResultRepository, OlympicResultRepository>();

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(WebApiAutoMapperProfile));
builder.Services.AddAutoMapper(typeof(BusinessAutoMapperProfile));
builder.Services.AddAutoMapper(typeof(RepositoryAutoMapperProfile));

builder.Services.AddDbContext<DataContext>(
    options => options.UseInMemoryDatabase("OlympicsDatabase")
);

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