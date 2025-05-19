using OfficeAnywhere.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
                Headers =
                    {
                        //{ "cookie", "ASP.NET_SessionId=uyvvuvzfbeca4pbnqsblpode; .delmon-egate.com=_YX9bybxURTl82Q1dgm3LUu4pPqQgTbhOVYCw8mrC1AYFAX_bT2L-GbQnxpiYTREyO2BGNMwBdW2BY8b3G5xT0fN1WteoJEVbsvHa-S7lOQwlbU91E_FI2KQuFhgLIT6ekFelPS4iv38eM7TP-gk_9SDWhwzx9s8IVHhF9TtNgEvIQ2OPlahQmrgPvJI31EPCAzU9a_eDUJRSPAnkKBSNppenmfC0lS0Da9VH7WGqkplWUlaEDpXjXMIrwDOOsAy5rspE-O55MeCRlTW1c-XL0npJJ1wFdJLZuf_Foi0LKonC2iktX40JneaLm5b5JqnAkmM3eam0OMmd8_ssYoAferE9Biqq5-TI127jgh0fOFXlpiIadaaKBCv25N3vzo48ohwBZiHkVsDnrjxlaF_QLPVBocqTXQ30Vi4Bz0klEciiYhUUIcRptOGMcnyTuA418YMbmIPFhZa90A0y90cjBwAjbNDQl2n0LzTPZImQn4" },
                    },
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
