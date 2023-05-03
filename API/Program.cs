using Application.ServiceRegistrations;
using Domain.ServiceRegistrations;
using Infrastructure.ServiceRegistrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
InfrastructureService.RegisterServices(builder.Services, builder.Configuration);
DomainService.RegisterServices(builder.Services);
ApplicationService.RegisterServices(builder.Services);

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
