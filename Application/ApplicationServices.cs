using Application.Helpers;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {
            service.AddScoped<ICarService, CarService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IRentalService, RentalService>();
            service.AddScoped<IUserCredentialsService, UserCredentialsService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IAuthenticationHelper, AuthenticationHelper>();

            return service;
        }
    }
}
