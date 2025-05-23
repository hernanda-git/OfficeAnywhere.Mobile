using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeAnywhere.Mobile.Models;

public class Form
{
    [JsonPropertyName("$id")]
    public string RefId { get; set; } = string.Empty;

    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("Priority")]
    public int Priority { get; set; }

    [JsonPropertyName("FormTemplateId")]
    public int FormTemplateId { get; set; }

    [JsonPropertyName("Active")]
    public bool Active { get; set; }

    [JsonPropertyName("IsProcess")]
    public bool IsProcess { get; set; }

    [JsonPropertyName("FormTemplate")]
    public FormTemplate FormTemplate { get; set; } = new();
}

public class FormTemplate
{
    [JsonPropertyName("$id")]
    public string RefId { get; set; }

    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Template")]
    public string Template { get; set; }

    // Helper property to deserialize Template JSON
    [JsonIgnore]
    public TemplateContent? TemplateContent
    {
        get => string.IsNullOrEmpty(Template) ? null : JsonSerializer.Deserialize<TemplateContent>(Template);
        set => Template = JsonSerializer.Serialize(value);
    }

    [JsonPropertyName("TemplateUrl")]
    public string? TemplateUrl { get; set; }

    [JsonPropertyName("HasTable")]
    public bool? HasTable { get; set; }

    [JsonPropertyName("TableName")]
    public string? TableName { get; set; }

    [JsonPropertyName("WebSiteUrl")]
    public string? WebSiteUrl { get; set; }

    [JsonPropertyName("Layout")]
    public string? Layout { get; set; }

    [JsonPropertyName("IsTask")]
    public bool? IsTask { get; set; }

    [JsonPropertyName("TaskToUser")]
    public string? TaskToUser { get; set; }

    [JsonPropertyName("TaskToRole")]
    public string? TaskToRole { get; set; }

    [JsonPropertyName("TaskTypeId")]
    public int? TaskTypeId { get; set; }

    [JsonPropertyName("AllowToEditOwiner")]
    public bool? AllowToEditOwner { get; set; }

    [JsonPropertyName("AllowToEditOnReplay")]
    public bool? AllowToEditOnReply { get; set; }

    [JsonPropertyName("ParentId")]
    public int? ParentId { get; set; }

    [JsonPropertyName("IsPrivate")]
    public bool? IsPrivate { get; set; }

    [JsonPropertyName("TaskToUserId")]
    public string? TaskToUserId { get; set; }
}

public class TemplateContent
{
    [JsonPropertyName("components")]
    public List<Component>? Components { get; set; }
}

public abstract class Component
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("input")]
    public bool Input { get; set; }

    [JsonPropertyName("tableView")]
    public bool TableView { get; set; }

    [JsonPropertyName("hidden")]
    public bool Hidden { get; set; }

    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; }

    [JsonPropertyName("validate")]
    public Validation Validate { get; set; } = new();

    [JsonPropertyName("conditional")]
    public Conditional Conditional { get; set; } = new();

    [JsonPropertyName("customClass")]
    public string CustomClass { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("tooltip")]
    public string Tooltip { get; set; } = string.Empty;
}

public class Validation
{
    [JsonPropertyName("required")]
    public bool Required { get; set; }

    [JsonPropertyName("custom")]
    public string Custom { get; set; } = string.Empty;

    [JsonPropertyName("customPrivate")]
    public bool CustomPrivate { get; set; }

    [JsonPropertyName("minLength")]
    public string MinLength { get; set; } = string.Empty;

    [JsonPropertyName("maxLength")]
    public string MaxLength { get; set; } = string.Empty;

    [JsonPropertyName("pattern")]
    public string Pattern { get; set; } = string.Empty;

    [JsonPropertyName("minWords")]
    public string MinWords { get; set; } = string.Empty;

    [JsonPropertyName("maxWords")]
    public string MaxWords { get; set; } = string.Empty;
}

public class Conditional
{
    [JsonPropertyName("show")]
    public string Show { get; set; } = string.Empty;

    [JsonPropertyName("when")]
    public string When { get; set; } = string.Empty;

    [JsonPropertyName("eq")]
    public string Eq { get; set; } = string.Empty;

    [JsonPropertyName("json")]
    public string Json { get; set; } = string.Empty;
}

public class HtmlElementComponent : Component
{
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("attrs")]
    public List<Attribute> Attrs { get; set; } = new();
}

public class Attribute
{
    [JsonPropertyName("attr")]
    public string Attr { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}

public class DataGridComponent : Component
{
    [JsonPropertyName("components")]
    public List<Component> Components { get; set; } = new();

    [JsonPropertyName("disableAddingRemovingRows")]
    public bool DisableAddingRemovingRows { get; set; }

    [JsonPropertyName("addAnother")]
    public string AddAnother { get; set; } = string.Empty;

    [JsonPropertyName("defaultValue")]
    public List<Dictionary<string, string>> DefaultValue { get; set; } = new();
}

public class TextFieldComponent : Component
{
    [JsonPropertyName("inputType")]
    public string InputType { get; set; } = string.Empty;

    [JsonPropertyName("inputFormat")]
    public string InputFormat { get; set; } = string.Empty;

    [JsonPropertyName("placeholder")]
    public string Placeholder { get; set; } = string.Empty;

    [JsonPropertyName("prefix")]
    public string Prefix { get; set; } = string.Empty;

    [JsonPropertyName("suffix")]
    public string Suffix { get; set; } = string.Empty;
}

public class TextAreaComponent : Component
{
    [JsonPropertyName("rows")]
    public int Rows { get; set; }

    [JsonPropertyName("wysiwyg")]
    public bool Wysiwyg { get; set; }

    [JsonPropertyName("inputFormat")]
    public string InputFormat { get; set; } = string.Empty;
}

public class ButtonComponent : Component
{
    [JsonPropertyName("action")]
    public string Action { get; set; } = string.Empty;

    [JsonPropertyName("theme")]
    public string Theme { get; set; } = string.Empty;

    [JsonPropertyName("size")]
    public string Size { get; set; } = string.Empty;

    [JsonPropertyName("disableOnInvalid")]
    public bool DisableOnInvalid { get; set; }
}

public class RadioComponent : Component
{
    [JsonPropertyName("values")]
    public List<RadioValue> Values { get; set; } = [];

    [JsonPropertyName("inline")]
    public bool Inline { get; set; }
}

public class RadioValue
{
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("shortcut")]
    public string Shortcut { get; set; } = string.Empty;
}

public class TableComponent : Component
{
    [JsonPropertyName("numRows")]
    public int NumRows { get; set; }

    [JsonPropertyName("numCols")]
    public int NumCols { get; set; }

    [JsonPropertyName("rows")]
    public List<List<TableCell>> Rows { get; set; } = [];

    [JsonPropertyName("striped")]
    public bool Striped { get; set; }

    [JsonPropertyName("bordered")]
    public bool Bordered { get; set; }
}

public class TableCell
{
    [JsonPropertyName("components")]
    public List<Component> Components { get; set; } = new();
}

public class FieldsetComponent : Component
{
    [JsonPropertyName("legend")]
    public string Legend { get; set; } = string.Empty;

    [JsonPropertyName("components")]
    public List<Component> Components { get; set; } = [];
}

public class SelectComponent : Component
{
    [JsonPropertyName("dataSrc")]
    public string DataSrc { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public SelectData Data { get; set; } = new();

    [JsonPropertyName("valueProperty")]
    public string ValueProperty { get; set; } = string.Empty;

    [JsonPropertyName("template")]
    public string Template { get; set; } = string.Empty;
}

public class SelectData
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("headers")]
    public List<Header> Headers { get; set; } = [];
}

public class Header
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}

public class ComponentConverter : JsonConverter<Component>
{
    public override Component? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;
        var type = root.GetProperty("type").GetString();

        return type switch
        {
            "htmlelement" => JsonSerializer.Deserialize<HtmlElementComponent>(root.GetRawText(), options),
            "datagrid" => JsonSerializer.Deserialize<DataGridComponent>(root.GetRawText(), options),
            "textfield" => JsonSerializer.Deserialize<TextFieldComponent>(root.GetRawText(), options),
            "textarea" => JsonSerializer.Deserialize<TextAreaComponent>(root.GetRawText(), options),
            "button" => JsonSerializer.Deserialize<ButtonComponent>(root.GetRawText(), options),
            "radio" => JsonSerializer.Deserialize<RadioComponent>(root.GetRawText(), options),
            "table" => JsonSerializer.Deserialize<TableComponent>(root.GetRawText(), options),
            "fieldset" => JsonSerializer.Deserialize<FieldsetComponent>(root.GetRawText(), options),
            "select" => JsonSerializer.Deserialize<SelectComponent>(root.GetRawText(), options),
            _ => JsonSerializer.Deserialize<Component>(root.GetRawText(), options)
        };
    }

    public override void Write(Utf8JsonWriter writer, Component value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}