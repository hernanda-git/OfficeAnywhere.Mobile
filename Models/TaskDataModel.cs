using System.Text.Json.Serialization;

namespace OfficeAnywhere.Mobile.Models;

public class TaskCard
{
    public string Title { get; set; } = string.Empty;
    public string TaskTypeName { get; set; } = string.Empty;
    public DateTime AddedDate { get; set; } = DateTime.MinValue;
    public DateTime? LastUpdatedDate { get; set; } = DateTime.MinValue;
    public string SenderUserImage { get; set; } = string.Empty;
    public string EmployUserImage { get; set; } = string.Empty;
    public bool NotSameImage { get; set; } = false;
    public int MessageCount { get; set; } = 0;
    public string DelmonTask { get; set; } = string.Empty;
}

public class TaskData
{
    [JsonPropertyName("Counts")]
    public int Counts { get; set; }

    [JsonPropertyName("DelmonTasks")]
    public List<DelmonTask> DelmonTasks { get; set; } = new List<DelmonTask>();

    [JsonPropertyName("Users")]
    public List<UserData> Users { get; set; } = new List<UserData>();
}

public class DelmonTask
{
    [JsonPropertyName("Values")]
    public List<object> Values { get; set; } = new List<object>();

    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("ParentId")]
    public int? ParentId { get; set; }

    [JsonPropertyName("Title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("Details")]
    public string Details { get; set; } = string.Empty;

    [JsonPropertyName("AddedDate")]
    public DateTime AddedDate { get; set; }

    [JsonPropertyName("TaskStateId")]
    public int TaskStateId { get; set; }

    [JsonPropertyName("TaskTypeId")]
    public int TaskTypeId { get; set; }

    [JsonPropertyName("Type")]
    public string Type { get; set; }

    [JsonPropertyName("Attach")]
    public string Attach { get; set; } = string.Empty;

    [JsonPropertyName("SenderUserImage")]
    public string SenderUserImage { get; set; } = string.Empty;

    [JsonPropertyName("EmployUserImage")]
    public string EmployUserImage { get; set; } = string.Empty;

    [JsonPropertyName("SenderId")]
    public int SenderId { get; set; }

    [JsonPropertyName("EmployId")]
    public int EmployId { get; set; }

    [JsonPropertyName("MainDocId")]
    public int? MainDocId { get; set; }

    [JsonPropertyName("TahseId")]
    public int? TahseId { get; set; }

    [JsonPropertyName("aqdid")]
    public double? Aqdid { get; set; }

    [JsonPropertyName("EmployName")]
    public string EmployName { get; set; } = string.Empty;

    [JsonPropertyName("SenderName")]
    public string SenderName { get; set; } = string.Empty;

    [JsonPropertyName("TaskStateName")]
    public string TaskStateName { get; set; } = string.Empty;

    [JsonPropertyName("TaskTypeName")]
    public string TaskTypeName { get; set; } = string.Empty;

    [JsonPropertyName("IsRead")]
    public bool? IsRead { get; set; }

    [JsonPropertyName("Priority")]
    public string Priority { get; set; } = string.Empty;

    [JsonPropertyName("Folder")]
    public int? Folder { get; set; }

    [JsonPropertyName("EndDate")]
    public DateTime? EndDate { get; set; }

    [JsonPropertyName("Color")]
    public string Color { get; set; } = string.Empty;

    [JsonPropertyName("Icon")]
    public string Icon { get; set; } = string.Empty;

    [JsonPropertyName("IsImportant")]
    public bool? IsImportant { get; set; }

    [JsonPropertyName("LastUpdatedDate")]
    public DateTime? LastUpdatedDate { get; set; }

    [JsonPropertyName("MessageCount")]
    public int MessageCount { get; set; }

    [JsonPropertyName("FormId")]
    public int? FormId { get; set; }

    [JsonPropertyName("FromData")]
    public string FromData { get; set; } = string.Empty;

    [JsonPropertyName("Messages")]
    public List<Message> Messages { get; set; } = new List<Message>();

    [JsonPropertyName("DocType")]
    public string DocType { get; set; } = string.Empty;

    [JsonPropertyName("Process")]
    public string Process { get; set; } = string.Empty;

    [JsonPropertyName("ProcessOrder")]
    public int? ProcessOrder { get; set; }

    [JsonPropertyName("IsRefues")]
    public bool? IsRefues { get; set; }

    [JsonPropertyName("IsPrivate")]
    public bool? IsPrivate { get; set; }

    [JsonPropertyName("closePermission")]
    public string ClosePermission { get; set; } = string.Empty;
}

public class Message
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("ParentId")]
    public int? ParentId { get; set; }

    [JsonPropertyName("Title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("Details")]
    public string Details { get; set; } = string.Empty;

    [JsonPropertyName("AddedDate")]
    public DateTime AddedDate { get; set; }

    [JsonPropertyName("TaskStateId")]
    public int TaskStateId { get; set; }

    [JsonPropertyName("TaskTypeId")]
    public int TaskTypeId { get; set; }

    [JsonPropertyName("Type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("Attach")]
    public string Attach { get; set; } = string.Empty;

    [JsonPropertyName("SenderId")]
    public int SenderId { get; set; }

    [JsonPropertyName("EmployId")]
    public int EmployId { get; set; }

    [JsonPropertyName("MainDocId")]
    public int? MainDocId { get; set; }

    [JsonPropertyName("EmployName")]
    public string EmployName { get; set; } = string.Empty;

    [JsonPropertyName("SenderName")]
    public string SenderName { get; set; } = string.Empty;

    [JsonPropertyName("TaskStateName")]
    public string TaskStateName { get; set; } = string.Empty;

    [JsonPropertyName("TaskTypeName")]
    public string TaskTypeName { get; set; } = string.Empty;

    [JsonPropertyName("IsRead")]
    public bool? IsRead { get; set; }

    [JsonPropertyName("Priority")]
    public string Priority { get; set; } = string.Empty;

    [JsonPropertyName("Folder")]
    public int? Folder { get; set; }

    [JsonPropertyName("EndDate")]
    public DateTime? EndDate { get; set; }

    [JsonPropertyName("Color")]
    public string Color { get; set; } = string.Empty;

    [JsonPropertyName("Icon")]
    public string Icon { get; set; } = string.Empty;

    [JsonPropertyName("IsImportant")]
    public bool? IsImportant { get; set; }

    [JsonPropertyName("LastUpdatedDate")]
    public DateTime? LastUpdatedDate { get; set; }

    [JsonPropertyName("MessageCount")]
    public int MessageCount { get; set; }

    [JsonPropertyName("FromData")]
    public string FromData { get; set; } = string.Empty;

    [JsonPropertyName("FormId")]
    public int? FormId { get; set; }

    [JsonPropertyName("EmployUserImage")]
    public string EmployUserImage { get; set; } = string.Empty;

    [JsonPropertyName("SenderUserImage")]
    public string SenderUserImage { get; set; } = string.Empty;

    [JsonPropertyName("DocType")]
    public string DocType { get; set; } = string.Empty;

    [JsonPropertyName("PlanId")]
    public int? PlanId { get; set; }

    [JsonPropertyName("IsPrivate")]
    public bool? IsPrivate { get; set; }

    [JsonPropertyName("TahseId")]
    public int? TahseId { get; set; }

    [JsonPropertyName("aqdId")]
    public double? AqdId { get; set; }

    [JsonPropertyName("DocName")]
    public string DocName { get; set; } = string.Empty;

    [JsonPropertyName("Process")]
    public string Process { get; set; } = string.Empty;

    [JsonPropertyName("ProcessOrder")]
    public int? ProcessOrder { get; set; }

    [JsonPropertyName("IsRefues")]
    public bool? IsRefues { get; set; }

    [JsonPropertyName("StartDate")]
    public DateTime? StartDate { get; set; }
}

public class UserData
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("UserName")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("Email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("Selected")]
    public bool Selected { get; set; }
}