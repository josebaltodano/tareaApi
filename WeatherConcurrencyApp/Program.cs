using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherConcurrencyApp.AppCore.Interfaces;
using WeatherConcurrencyApp.AppCore.Services;
using WeatherConcurrencyApp.Infrastructure.OpenWeatherClient;
using WeatherConcurrentApp.Domain.Interfaces;

namespace WeatherConcurrencyApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();
            builder.RegisterType<HttpOpenWeatherClient>().As<IHttpOpenWeatherClient>();
            builder.RegisterType<HttpOpenWeatherClientService>().As<IHttpOpenWeatherClientService>();
            var container = builder.Build();

            Application.Run(new FrmMain(container.Resolve<IHttpOpenWeatherClientService>()));
        }
    }
}
