using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CrossCutting
{
    public partial class AppSettings
    {
        public static IConfiguration Configuration { get; set; }
        public static string ConnectionString { get; set; }
        public static string RootPath { get; set; }
        public static int TimerElapsed { get; set; }

        //public AppSettings(IConfiguration config)
        //{
        //    Configuration = config;
        //    ConnectionString = config.GetConnectionString("DefaultConnection");
        //    RootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        //}

        public AppSettings(IConfiguration config)
        {
            Configuration = config;
            ConnectionString = config.GetConnectionString("DefaultConnection");
            RootPath = config.GetSection("RootPah").Value;
            TimerElapsed = Convert.ToInt32(config.GetSection("TimerElapsed").Value);
        }
    }
}
