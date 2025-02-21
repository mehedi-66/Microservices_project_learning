using PlatformServices.Dtos;
using System.Text;
using System.Text.Json;

namespace PlatformServices.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
         async Task ICommandDataClient.SendPlatformToCommand(PlatformReadDto platform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json"
            );
            Console.WriteLine($"command service run on {_configuration["CommandServiceBaseUrl"]}");
            var response = await _httpClient.PostAsync($"{_configuration["CommandServiceBaseUrl"]}/api/c/Platforms", httpContent);
            
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CommandService was OK");
            }
            else
            {
                Console.WriteLine("--> Sync POST to CommandService was NOT OK");
            }
        }
    }
}
