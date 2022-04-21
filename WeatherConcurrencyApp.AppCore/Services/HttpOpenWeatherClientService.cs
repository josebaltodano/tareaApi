using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherConcurrencyApp.AppCore.Interfaces;
using WeatherConcurrentApp.Domain.Entities;
using WeatherConcurrentApp.Domain.Interfaces;

namespace WeatherConcurrencyApp.AppCore.Services
{
    public class HttpOpenWeatherClientService : IHttpOpenWeatherClientService
    {
        private IHttpOpenWeatherClient openWeatherClient;

        public HttpOpenWeatherClientService(IHttpOpenWeatherClient openWeatherClient)
        {
            this.openWeatherClient = openWeatherClient;
        }

        public string GetImage(OpenWeather ow)
        {
            return openWeatherClient.GetImage(ow);
        }

        public Task<OpenWeather> GetWeatherByCityNameAsync(string city)
        {
            return openWeatherClient.GetWeatherByCityNameAsync(city);
        }
    }
}
