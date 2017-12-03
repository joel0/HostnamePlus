using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace HostnamePlus
{
    public class Program
    {
        public const String BASE_URL = "hostname.jmay.us";
        public static String getOtherIpJsPath;
        public static String mainCssPath;

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
