namespace OfficeAnywhere.Mobile.Models;

public class MainUser
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; } = true;
    public string PasswordHash { get; set; } = string.Empty;
    public string SecurityStamp { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? LockoutEndDateUtc { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserImage { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string MobileRole { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}