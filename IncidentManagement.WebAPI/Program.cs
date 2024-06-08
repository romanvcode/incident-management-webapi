using IncidentManagement.Core.RepositoryContracts;
using IncidentManagement.Core.ServiceContracts;
using IncidentManagement.Core.Services;
using IncidentManagement.Infrastructure.DatabaseContext;
using IncidentManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
});

builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<IIncidentAdderService, IncidentAdderService>();
builder.Services.AddScoped<IIncidentGetterService, IncidentGetterService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountAdderService, AccountAdderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
