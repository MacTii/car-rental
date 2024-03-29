﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Repositories;
using Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CarRentalDatabase"));
            });

            service.AddTransient<DataSeeder>();

            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<ICarRepository, CarRepository>();
            service.AddScoped<IRentalRepository, RentalRepository>();
            service.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();
            service.AddScoped<IBlogRepository, BlogRepository>();
            service.AddScoped<ICommentRepository, CommentRepository>();
            service.AddScoped<IRawQueryRepository, RawQueryRepository>();

            return service;
        }

        public static IServiceProvider SeedDatabase(this IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                dataSeeder.SeedData();
            }

            return service;
        }
    }
}
