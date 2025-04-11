using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleHandler;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging();
        serviceCollection.AddHandlers();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var realHandler = serviceProvider.GetService<RealHandler>()
                          ?? throw new NotImplementedException();

        var realResponse = await realHandler.Handle(new RealRequest() { Something = "abc" });

        Console.WriteLine(realResponse.Payload);
    }

    private static ServiceCollection AddHandlers(this ServiceCollection serviceCollection)
    {
        // Get all assemblies
        var assemblies = new List<Assembly>();
        assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
        assemblies.AddRange(Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load));

        // Find all handlers
        var handlers = assemblies.SelectMany(s => s.GetTypes())
            .Where(t => t.IsClass
                        && !t.IsAbstract
                        && t.BaseType != null
                        && t.BaseType.IsGenericType
                        && t.BaseType.GetGenericTypeDefinition() == typeof(AbstractHandler<,,>));

        foreach (var handler in handlers)
        {
            serviceCollection.AddTransient(handler, handler);
        }

        return serviceCollection;
    }
}
