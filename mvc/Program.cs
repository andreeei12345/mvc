using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;
namespace mvc
{
    public class Program
    {

        async public static void retime()
        {
            while (true)
            {
                var oneSecond = Task.Delay(TimeSpan.FromSeconds(10));
                await oneSecond;
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.addrequest();
                }
            }
        }
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
           
        }
      
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    retime();
                });
    }
}
