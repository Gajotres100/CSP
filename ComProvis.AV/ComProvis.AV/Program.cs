using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ComProvis.AV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                            .ConfigureAppConfiguration((context, builder) =>
                            {
                                var env = context.HostingEnvironment;
                                builder.AddJsonFile("appsettings.json",
                                                    optional: true, reloadOnChange: true)
                                              .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                                                    optional: true, reloadOnChange: true);
                                builder.AddEnvironmentVariables();
                            })
                .UseStartup<Startup>()
                .Build();
    }
}
