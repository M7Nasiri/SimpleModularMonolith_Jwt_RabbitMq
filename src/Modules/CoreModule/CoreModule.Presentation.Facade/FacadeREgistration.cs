using Microsoft.Extensions.DependencyInjection;
using Presentation.Facade.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreModule.Presentation.Facade
{
    public static class FacadeRegistration
    {
        public static void InitCoreModuleFacade(this IServiceCollection services)
        {
            services.AddScoped<IPostFacade, PostFacade>();
        }
    }
}
