using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using SuperSocket;
using SuperSocket.Command;
using SuperSocket.WebSocket.Server;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDM.WebSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            //using var client = await StartClientWithRetries();

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseKestrel(op =>
                    {
                        op.ListenAnyIP(3008);
                    })
                    .UseStartup<Startup>();
                })
                .AsMultipleServerHostBuilder()
                .AddWebSocketServer<MDMServer>(ss =>
                {
                    ss.UseCommand<MDMPackageInfo, MDMPackageConverter>(commandOptions =>
                    {
                        // register commands one by one
                        commandOptions.AddCommand<Resp>();
                        commandOptions.AddCommand<Unknown>();
                    })
                    .UseHostedService<MDMServer>()
                    .UseSession<MDMSession>()
                    .UseInProcSessionContainer()
                    .ConfigureAppConfiguration((hostCtx, configApp) =>
                    {
                        configApp.AddInMemoryCollection(new Dictionary<string, string>
                        {
                            { "serverOptions:name", "MDMServer" },
                            { "serverOptions:listeners:0:ip", "Any" },
                            { "serverOptions:listeners:0:port", "4040" }
                        });

                    });
                })
                .ConfigureLogging((hostCtx, loggingBuilder) =>
                {
                    loggingBuilder.AddConsole();
                })
                .Build();


            host.Run();
        }

        //private static async Task<IClusterClient> StartClientWithRetries(int initializeAttemptsBeforeFailing = 5)
        //{
        //    int attempt = 0;
        //    IClusterClient client;
        //    while (true)
        //    {
        //        try
        //        {
        //            var builder = new ClientBuilder()
        //                .UseLocalhostClustering()
        //                .ConfigureApplicationParts(parts =>
        //                    parts.AddApplicationPart(typeof(IAccount).Assembly).WithReferences()
        //                    .AddFromApplicationBaseDirectory()
        //                    )
        //                .ConfigureLogging(logging => logging.AddConsole());
        //            client = builder.Build();
        //            await client.Connect();
        //            Console.WriteLine("Client successfully connect to silo host");
        //            break;
        //        }
        //        catch (Exception ex)
        //        {
        //            attempt++;
        //            Console.WriteLine(
        //                $"Attempt {attempt} of {initializeAttemptsBeforeFailing} failed to initialize the Orleans client.");
        //            if (attempt > initializeAttemptsBeforeFailing)
        //            {
        //                throw;
        //            }

        //            await Task.Delay(TimeSpan.FromSeconds(5));
        //        }
        //    }

        //    return client;
        //}
    }
}
