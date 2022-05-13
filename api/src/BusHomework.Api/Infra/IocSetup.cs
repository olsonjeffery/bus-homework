using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusHomework.Api.Infra
{
  public class IocSetup
  {
    public IocSetup() { }

    public static void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
      services.Scan(scan =>
          scan.FromAssemblyOf<Program>()
              .AddClasses()
              .AsImplementedInterfaces()
              .WithScopedLifetime()
      );
    }
  }
}
