using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeAnywhere.Mobile.Models;

public class UserAuthModel
{
    [JsonPropertyName("msg")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("UserName")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("Email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("Tags")]
    public string Tags { get; set; } = string.Empty;

    [JsonPropertyName("tenant")]
    public string Tenant { get; set; } = string.Empty;

    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; } = [];

    [JsonPropertyName("primistion")]
    public string PermissionsJson { get; set; } = string.Empty;

    [JsonPropertyName("Tenants")]
    public List<object> Tenants { get; set; } = [];

    [JsonIgnore]
    public List<Permission>? Permissions
    {
        get
        {
            if (string.IsNullOrEmpty(PermissionsJson))
                return new List<Permission>();

            try
            {
                return JsonSerializer.Deserialize<List<Permission>>(
                    PermissionsJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                //Console.WriteLine($"Error deserializing Permissions: {ex.Message}");
                return new List<Permission>();
            }
        }
    }
}

public class Permission
{
    [JsonPropertyName("CompName")]
    public string ComponentName { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("roles")]
    public PermissionRoles? Roles { get; set; }
}

public class PermissionRoles
{
    [JsonPropertyName("view")]
    public bool View { get; set; }

    [JsonPropertyName("edit")]
    public bool Edit { get; set; }
}