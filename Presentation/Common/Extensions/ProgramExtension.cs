using Application;
using Infrastructure;

namespace Presentation.Common.Extensions;

public static class BuilderExtensions
{
    public static WebApplicationBuilder CreateBuilder(string[] args)
    {
        return WebApplication.CreateBuilder(args);
    }

    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddInfrastructure(builder.Configuration)
            .AddApplication();
            
        builder.Services
            .AddPolicies();

        builder.Services
            .AddHttpContextAccessor()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
            
        builder.Services
            .AddControllers();
        
        return builder;
    }

    public static WebApplication Build(this WebApplicationBuilder builder)
    {
        return builder.Build();
    }

    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app
                .UseSwagger()
                .UseSwaggerUI();
        }

        app
            .UseHttpsRedirection()
            .UseAuthentication()
            .UseAuthorization();
            
        app
            .MapControllers();

        return app;
    }

    public static void Run(this WebApplication app)
    {
        app.Run();
    }
}