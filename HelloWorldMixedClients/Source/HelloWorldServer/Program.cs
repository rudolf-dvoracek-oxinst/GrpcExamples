namespace HelloWorldServer;

using System.Net;
using HelloWorldServer.GrpcServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        // Create a new host builder
        var builder = Host.CreateDefaultBuilder(args)
            // Configure the host to use Windows services
            .UseWindowsService(options => options.ServiceName = @"Hello World Service")
            // Configure the host to use a windows service hosted
            .ConfigureServices(services =>
            {
                services.AddHostedService<HelloWorldBackgroundService>();
            })
            .ConfigureWebHostDefaults(hostBuilder =>
            {
                hostBuilder.Configure((context, applicationBuilder) =>
                    {
                        applicationBuilder.UseRouting();

                        // Enable GrpcWeb support
                        applicationBuilder.UseGrpcWeb();
                        applicationBuilder.UseEndpoints(routeBuilder =>
                        {
                            routeBuilder.MapGrpcService<HelloWorldService>().EnableGrpcWeb();
                        });
                    });

                hostBuilder.ConfigureKestrel(options =>
                    options.ListenLocalhost(5069, listenOptions =>
                    {
                        // Use secure channel to support simultaneous http2 and http1.1
                        listenOptions.UseHttps();

                        // Set http2 and http1.1 as the protocols
                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    }));

                hostBuilder.ConfigureServices(collection =>
                {
                    collection.AddGrpc(options =>
                    {
                        options.EnableDetailedErrors = true;
                    });
                });
            });

        var app = builder.Build();
        app.Run();
    }
}