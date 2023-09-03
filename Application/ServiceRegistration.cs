using Application.Helpers;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {
            service.AddScoped<ICarService, CarService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IRentalService, RentalService>();
            service.AddScoped<IUserCredentialsService, UserCredentialsService>();
            service.AddScoped<IBlogService, BlogService>();
            service.AddScoped<ICommentService, CommentService>();

            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IRegistrationService, RegistrationService>();
            service.AddScoped<IPasswordService, PasswordService>();
            service.AddScoped<IUserContextService, UserContextService>();

            service.AddScoped<IPasswordHelper, PasswordHelper>();
            service.AddScoped<ITokenHelper, TokenHelper>();
            service.AddScoped<IUserContextHelper, UserContextHelper>();

            service.AddScoped<IBackupRestoreService, BackupRestoreService>();

            return service;
        }
    }
}
