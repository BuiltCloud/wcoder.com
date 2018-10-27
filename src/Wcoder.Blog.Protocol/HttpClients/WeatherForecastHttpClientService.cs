using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Wcoder.Blog.Protocol.Interfaces;
using Wcoder.Blog.Protocol.Models;

namespace Wcoder.Blog.Protocol.HttpClients
{
    public class WeatherForecastHttpClientService : IWeatherForecastService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly string controllerName = "WeatherForecastService";
        private readonly string clientName = "WcoderBlog";

        public WeatherForecastHttpClientService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var url = $"/{controllerName}/{nameof(GetForecastAsync)}";
            var client = httpClientFactory.CreateClient(clientName);
            var r = await client.GetAsync(url);
            using (var stream = await r.Content.ReadAsStreamAsync())
            {
                DataContractJsonSerializer jsonSerialize = new DataContractJsonSerializer(typeof(WeatherForecast[]));
                return (WeatherForecast[])jsonSerialize.ReadObject(stream);
            }
        }
    }
}