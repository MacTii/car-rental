var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Infrastructure.ServiceRegistration.AddInfrastructureServices(builder.Services, builder.Configuration);
Domain.ServiceRegistration.AddDomainServices(builder.Services);
Application.ServiceRegistration.AddApplicationServices(builder.Services);

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
