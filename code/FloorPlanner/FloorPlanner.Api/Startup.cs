
using Core.Context.RequestContext;
using FloorPlanner.Api.Context;
using FloorPlanner.Api.Extensions;
using FloorPlanner.Api.Middlewares;
using FloorPlanner.Bll;
using FloorPlanner.Bll.Mappings;
using FloorPlanner.Dal;
using Microsoft.Extensions.Primitives;
using System.Text.Json.Serialization;

namespace FloorPlanner.Api;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddLogging();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSwaggerDocument();

        services.AddSingleton(_ => MapperConfig.ConfigureAutoMapper());

        services.AddHttpContextAccessor();
        services.AddMemoryCache();

        services.AddDal(_configuration.GetConnectionString("DefaultConnection")!);

        services.AddWindowsAuthenticationAndAuthorization(_configuration); // Windows Authentication

        services.AddScoped<ICurrentUserIdContext, CurrentUserContext>();
        services.AddScoped<ICurrentUserContext, CurrentUserContext>();
        services.AddScoped<IRequestContext, RequestContext>();
        services.AddBllServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor contextAccessor)
    {
        app.UseExceptionHandler(app => app.UseMiddleware<ErrorHandlerMiddleware>());

        if (env.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseHsts();
        }

        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("X-Content-Type-Options", new StringValues("nosniff"));
            context.Response.Headers.Add("X-Frame-Options", new StringValues("SAMEORIGIN"));
            context.Response.Headers.Add("X-XSS-Protection", new StringValues("1; mode=block"));
            context.Response.Headers.Add("Cache-Control", new StringValues("no-store, no-cache"));
            context.Response.Headers.Add("Pragma", new StringValues("no-cache"));

            context.Response.Headers.Add("X-Developed-By", "Janos Varga");

            await next.Invoke();
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<AuthenticationMiddleware>();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapFallbackToFile("index.html");
        });
    }
}