using Application.Posts.Create;
using Application.Users;
using Application.Users.Register;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Common.AspNetCore;
using Common.EventBus.Abstractions;
using Common.EventBus.RabbitMQ;
using CoreModule.Domain.PostAgg.Repository;
using CoreModule.Infrastructure.Persistent.EF.PostAgg;
using CoreModule.Query.Posts.GetByFilter;
using Domain.RoleAgg.Repository;
using Domain.UserAgg.Repository;
using Domain.UserAgg.Services;
using Infrastructure;
using Infrastructure.Persistent.Dapper;
using Infrastructure.Persistent.EF.RoleAgg;
using Infrastructure.Persistent.EF.UserAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Infrastructure.JwtUtil;
using Presentation.Facade.Posts;
using Query.Users.GetByFilter;


namespace MyApi;

public static class ServiceRegistration
{
    public static void InitConfig(this IServiceCollection services, string connectionString)
    {
        services.AddControllers()
            .ConfigureApiBehaviorOptions(option =>
            {
                option.InvalidModelStateResponseFactory = (context =>
                {
                    var result = new ApiResult()
                    {
                        IsSuccess = false,
                        MetaData = new()
                        {
                            AppStatusCode = AppStatusCode.BadRequest,
                            Message = ModelStateUtil.GetModelStateErrors(context.ModelState)
                        }
                    };
                    return new BadRequestObjectResult(result);
                });
            });

        services.AddSingleton<IEventBus, EventBusRabbitMQ>();

        services.AddScoped<CustomJwtValidation>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IUserDomainService, UserDomainService>();
        services.AddScoped<IFileService, FileService>();

        services.AddMediatR(cfg =>
        {

            cfg.RegisterServicesFromAssembly(typeof(CreatePostCommand).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);

            cfg.RegisterServicesFromAssembly(typeof(GetUserByFilterQuery).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetPostByFilterQuery).Assembly);
        });
        
        services.AddTransient(_ => new DapperContext(connectionString));
        services.AddDbContext<DbCtx>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}
