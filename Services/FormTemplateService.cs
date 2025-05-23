using Microsoft.Maui.ApplicationModel.Communication;
using OfficeAnywhere.Mobile.Models;
using System.Text;
using System.Text.Json;

namespace OfficeAnywhere.Mobile.Services;

public class FormTemplateService
{
    private readonly HttpClient _client;

    public FormTemplateService()
    {
        var clientHandler = new HttpClientHandler
        {
            UseCookies = false,
        };
        _client = new HttpClient(clientHandler);
    }

    public async Task<List<UserModelV2>?> FetchUsers()
    {
        List<UserModelV2>? res = new List<UserModelV2>();
        
        try
        {
            string tenant = await SecureStorage.GetAsync("Tenant") ?? "";
            string accessToken = await SecureStorage.GetAsync("AccessToken") ?? "";

            var clientHandler = new HttpClientHandler
            {
                UseCookies = false,
            };
            var client = new HttpClient(clientHandler);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://o-anywhere.com//api/MainUsers?%24select=Id%2CUserName"),
                Headers =
            {
                { "tenant", tenant },
                { "Authorization", $"Bearer {accessToken}" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                MainUsersResponse? usersResponse = JsonSerializer.Deserialize<MainUsersResponse?>(body);
                if(usersResponse != null)
                {
                    res = usersResponse.Value;
                }
            }
        }
        catch(Exception ex)
        {
            //throw new Exception("Error fetching users", ex);
        }

        return res;
    }

    public async Task<List<Form>?> FetchTaskType()
    {
        List<Form>? res = new List<Form>();

        try
        {
            string tenant = await SecureStorage.GetAsync("Tenant") ?? "";
            string accessToken = await SecureStorage.GetAsync("AccessToken") ?? "";

            var clientHandler = new HttpClientHandler
            {
                UseCookies = false,
            };
            var client = new HttpClient(clientHandler);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://o-anywhere.com/api/TaskTypes1/%202"),
                Headers =
            {
                { "tenant", tenant },
                { "Authorization", $"Bearer {accessToken}" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                res = JsonSerializer.Deserialize<List<Form>?>(body);
            }
        }
        catch(Exception ex)
        {
            throw new Exception("Error fetching task type", ex);
        }

        return res;
    }

}
