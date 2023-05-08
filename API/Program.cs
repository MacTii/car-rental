using Infrastructure;
using Domain;
using Application;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
InfrastructureServices.AddInfrastructureServices(builder.Services, builder.Configuration);
DomainServices.AddDomainServices(builder.Services);
ApplicationServices.AddApplicationServices(builder.Services);

// Register middleware service
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Seed database
InfrastructureServices.SeedDatabase(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
