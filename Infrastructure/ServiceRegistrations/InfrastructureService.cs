using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServiceRegistrations
{
    public static class InfrastructureService
    {
        public static void RegisterServices(IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<CarRentalContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CarRentalDatabase"));
            });

            service.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
