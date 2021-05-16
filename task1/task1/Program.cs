using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace task1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
            var emailinfo = new EmailConnectionInfo()
            {
                FromEmail = "samplemail@gmail.com",
                ToEmail = "armanhossain54546@gmail.com",
                MailServer = "smtp.gmail.com",
                NetworkCredentials = new NetworkCredential()
                {
                    UserName = "samplemail@gmail.com",
                    Password = "samplepass",
                },
                EnableSsl = true,
                Port = 465,
                EmailSubject = "Applicationn Logs"
            };

         Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(configBuilder)
            .WriteTo.Email(emailinfo,outputTemplate: "{Timestamp:yyyy-MM-ddHH: mm:ss.fff zzz}[{Level}] {Message}{ NewLine}{ Exception}",LogEventLevel
            .Information,batchPostingLimit: 1)
            .CreateLogger();

            try
            {
                Log.Information("Application Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:80");
                });
    }
}
