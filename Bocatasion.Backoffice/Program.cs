using Bocatasion.Backoffice.Interfaces;
using Bocatasion.Backoffice.Services.Food;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bocatasion.Backoffice
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var http = new HttpClient { BaseAddress = new Uri("https://localhost:44374"), };
            http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            builder.Services.AddScoped(sp => http);
            builder.Services.AddTransient<IFoodService, FoodService>();

            await builder.Build().RunAsync();
        }
    }
}
