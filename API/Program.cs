var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Infrastructure.InfrastructureServices.AddInfrastructureServices(builder.Services, builder.Configuration);
Domain.DomainServices.AddDomainServices(builder.Services);
Application.ApplicationServices.AddApplicationServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Seed database
Infrastructure.InfrastructureServices.SeedDatabase(app.Services);

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
