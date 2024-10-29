using Microsoft.EntityFrameworkCore;
using Olympics.Business;
using Olympics.Business.AutoMapper;
using Olympics.Business.Interfaces;
using Olympics.DataAccess;
using Olympics.DataAccess.Seeder;
using Olympics.Repository;
using Olympics.Repository.AutoMapper;
using Olympics.Repository.Interfaces;
using Olympics.WebApi.AutoMapper;

const string corsLocalhostPolice = "_allowLocalhostOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsLocalhostPolice,
        policy  =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register business
builder.Services.AddScoped<IOlympicResultBusiness, OlympicResultBusiness>();

//Register repositories
builder.Services.AddScoped<IOlympicResultRepository, OlympicResultRepository>();

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(WebApiAutoMapperProfile));
builder.Services.AddAutoMapper(typeof(BusinessAutoMapperProfile));
builder.Services.AddAutoMapper(typeof(RepositoryAutoMapperProfile));

builder.Services.AddDbContext<DataContext>(
    options => options.UseInMemoryDatabase("OlympicsDatabase")
);
builder.Services.AddTransient<DataSeeder>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsLocalhostPolice);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    seeder.Seed();
}

app.Run();