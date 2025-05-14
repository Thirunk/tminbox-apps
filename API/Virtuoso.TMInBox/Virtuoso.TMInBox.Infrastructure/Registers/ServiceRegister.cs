using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Virtuoso.TMInBox.Infrastructure.Registers
{
    public static class ServiceRegister
    {

        public static IServiceCollection AddTMInBoxInfrastructure(this IServiceCollection services)
        {
            services.RegisterRepositoryAndServices();
            return services;
        }


        public static void RegisterRepositoryAndServices(this IServiceCollection services)
        {
            var domainEventHandlersTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(s => !s.IsInterface && (s.Name.EndsWith("Repository") || s.Name.EndsWith("Service")));
            foreach (var domain in domainEventHandlersTypes)
            {
                foreach (var implementedInterface in domain.GetInterfaces().Where(a=>a.Name.EndsWith("Repository") || a.Name.EndsWith("Service"))) 
                {
                    services.AddTransient(implementedInterface, domain);
                        
                }
            }
        }

    }
}
