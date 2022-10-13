using Net.Scaffolder.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Net.Scaffolder.Tests
{
public class HelloWorldUnitTest
{
    private readonly HelloWorldController _helloWorldController;

    public HelloWorldUnitTest()
    {
        ILogger<HelloWorldController> logger = new Logger<HelloWorldController>(new NullLoggerFactory());
        var configurations = new ConfigurationBuilder().Build();
        _helloWorldController = new HelloWorldController(logger, configurations);
    }

    [Fact]
    public void GetSuccess()
    {
        var result = _helloWorldController.Get();
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsType<string>(result);
        Assert.Equal("Hello world", result);
    }
}
}