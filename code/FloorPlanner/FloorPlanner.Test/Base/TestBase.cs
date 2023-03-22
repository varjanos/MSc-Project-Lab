using Core.Context.CurrentUserIdContext;
using Core.Context.RequestContext;
using FloorPlanner.Bll;
using FloorPlanner.Bll.Mappings;
using FloorPlanner.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FloorPlanner.Test.Base;

public class TestBase
{
    private readonly ServiceCollection _services;
    private readonly IConfigurationRoot _config;
    private readonly IHostEnvironment _environment;
    private readonly IServiceScope _scope;

    protected TestBase()
    {
        _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables("TEST_")
            .Build();

        _environment = new TestHostEnvironment();

        _services = new ServiceCollection();
        _services.AddLogging();

        _services.AddScoped<IRequestContext, TestRequestContext>();
        _services.AddScoped<ICurrentUserIdContext, TestCurrentUserIdContext>();
        _services.AddSingleton(_ => MapperConfig.ConfigureAutoMapper());
        _services.AddMemoryCache();

        _services.AddDal(_config.GetConnectionString("TestDefaultConnection"));

        _services.AddBllServices();

        var serviceProvider = _services.BuildServiceProvider(new ServiceProviderOptions()
        {
            ValidateScopes = true,
            ValidateOnBuild = true,
        });


        _scope = serviceProvider.CreateScope();
        ServiceProvider = _scope.ServiceProvider;
    }

    protected IServiceProvider ServiceProvider { get; }
}
