using Microsoft.Maui.ApplicationModel.Communication;
using OfficeAnywhere.Mobile.Models;
using System.Text;
using System.Text.Json;

namespace OfficeAnywhere.Mobile.Services;

public class TaskService
{
    private readonly HttpClient _client;

    public TaskService()
    {
        var clientHandler = new HttpClientHandler
        {
            UseCookies = false,
        };
        _client = new HttpClient(clientHandler);
    }

    public async Task<MainUser?> FetchMainUser()
    {
        MainUser res = new MainUser();
        string userId = await SecureStorage.GetAsync("UserId") ?? "0";
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
            RequestUri = new Uri($"https://o-anywhere.com/api/MainUsers({userId})"),
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

            var mainUser = JsonSerializer.Deserialize<MainUser>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (mainUser is not null)
            {
                res = mainUser;
                await SecureStorage.SetAsync("UserImage", mainUser.UserImage);
                await SecureStorage.SetAsync("PhoneNumber", mainUser.PhoneNumber);
            }

            return res;
        }
    }

    public async Task<TaskData?> FetchTaskData(CancellationToken cancellationToken = default)
    {
        TaskData res = new TaskData();

        try
        {
            string userId = await SecureStorage.GetAsync("UserId") ?? "0";
            string tenant = await SecureStorage.GetAsync("Tenant") ?? "";
            string accessToken = await SecureStorage.GetAsync("AccessToken") ?? "";

            using var clientHandler = new HttpClientHandler
            {
                UseCookies = false,
            };
            using var client = new HttpClient(clientHandler);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://o-anywhere.com/api/Global/GetFolders?=&id={userId}&folder=1&state=0&skip=0&take=10&type=0&planid=0&Project=&Customer=&keySearch="),
                Headers =
            {
                { "tenant", tenant },
                { "Authorization", $"Bearer {accessToken}" },
            },
            };

            using var response = await client.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync(cancellationToken);

            var data = JsonSerializer.Deserialize<TaskData>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data is not null)
            {
                res = data;
            }
        }
        catch (OperationCanceledException)
        {
            return null;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Network error occurred while fetching tasks.", ex);
        }
        catch (JsonException ex)
        {
            throw new Exception("Error parsing task data.", ex);
        }

        return res;
    }

    public async Task<string?> FetchTaskDataString()
    {
        string res = string.Empty;
        string userId = await SecureStorage.GetAsync("UserId") ?? "0";
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
            RequestUri = new Uri($"https://o-anywhere.com/api/Global/GetFolders?=&id={userId}&folder=1&state=0&skip=0&take=10&type=0&planid=0&Project=&Customer=&keySearch="),
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
            res = body;
        }

        return res;
    }
}
