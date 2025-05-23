using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OfficeAnywhere.Mobile.Models;

public enum FontSizeOption
{
    Large = 28,
    Medium = 22,
    Description = 16
}

public enum InputType
{
    Text,
    Email,
    Numeric,
    Telephone,
    Url,
    Chat
}

public class UserModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
}

public class MainUsersResponse
{
    [JsonPropertyName("@odata.context")]
    public string OdataContext { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public List<UserModelV2> Value { get; set; } = new List<UserModelV2>();
}

public class UserModelV2
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("UserName")]
    public string UserName { get; set; } = string.Empty;
}
