using OfficeAnywhere.Mobile.Models;
using System.Text;
using System.Text.Json;

namespace OfficeAnywhere.Mobile.Services;

public class AuthService
{
    private readonly HttpClient _client;

    public AuthService()
    {
        var clientHandler = new HttpClientHandler
        {
            UseCookies = false,
        };
        _client = new HttpClient(clientHandler);
    }

    public async Task<UserAuthModel> LoginAsync(string email, string password)
    {
        try
        {
            UserAuthModel res = new UserAuthModel();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://o-anywhere.com/login/Token"),
                Content = new StringContent(
                    JsonSerializer.Serialize(new { Email = email, PassWord = password }),
                    Encoding.UTF8,
                    "application/json")
            };

            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var authResult = JsonSerializer.Deserialize<UserAuthModel>(body, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if(authResult is not null)
                {
                    res = authResult;
                }
                return res;
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP Error: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Deserialization Error: {ex.Message}");
            throw;
        }
    }
}
