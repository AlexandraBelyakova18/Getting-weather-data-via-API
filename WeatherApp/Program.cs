using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WeatherApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiKey = "34c36a41d02d20d60370534585000587"; // Ваш API-ключ
        private const string baseUrl = "http://api.openweathermap.org/data/2.5/weather";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Введите название города:");
            string city = Console.ReadLine();
            await GetWeatherData(city);
        }

        private static async Task GetWeatherData(string city)
        {
            string url = $"{baseUrl}?q={city}&appid={apiKey}&units=metric"; // В metric - температура в °C
            try
            {
                var response = await client.GetStringAsync(url);
                JObject weatherData = JObject.Parse(response);

                var temperature = (float)weatherData["main"]["temp"];
                var humidity = (int)weatherData["main"]["humidity"];
                var weatherDescription = (string)weatherData["weather"][0]["description"];

                Console.WriteLine($"Температура: {temperature} °C");
                Console.WriteLine($"Влажность: {humidity} %");
                Console.WriteLine($"Описание погоды: {weatherDescription}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Ошибка при получении данных: " + e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
        }
    }
}
