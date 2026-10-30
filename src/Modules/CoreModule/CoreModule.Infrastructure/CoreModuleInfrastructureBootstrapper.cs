using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using CoreModule.Infrastructure.EventHandlers;
using CoreModule.Infrastructure.Persistent.EF;

namespace CoreModule.Infrastructure
{
    public static class CoreModuleInfrastructureBootstrapper
    {
        public static void RegisterDependency(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHostedService<UserRegisteredEventHandler>();
            services.AddDbContext<CoreModuleCtx>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("CoreModuleDbConnection"));
            });
        }
    }
}
