using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace ICMSRestore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //public static void Main(string[] args)
        //{
        //    CreateWebHostBuilder(args).Build().Run();
        //}

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //        .UseKestrel(serverOptions =>
        //        {
        //            serverOptions.Limits.MaxResponseBufferSize = long.MaxValue;
        //            serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
        //            serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromHours(1);
        //            serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromHours(1);
        //        });
    }
}
