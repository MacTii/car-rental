using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection service)
        {
            return service;
        }
    }
}
