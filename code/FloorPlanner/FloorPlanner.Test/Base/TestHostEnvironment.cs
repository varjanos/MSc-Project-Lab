using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace FloorPlanner.Test.Base;

public class TestHostEnvironment : IHostEnvironment
{
    public string EnvironmentName { get; set; } = "TEST";

    public string ApplicationName { get; set; }

    public string ContentRootPath { get; set; }

    IFileProvider IHostEnvironment.ContentRootFileProvider { get; set; }
}