using FocusXp.Domain.Enum;

namespace FocusXp.Domain.Extensions;

public static class EnumExtensions
{
    public static string GetRoleString(this Role role)
    {
        return role switch
        {
            Role.Admin => "Admin",
            Role.User => "User",
            Role.PremiumUser => "PremiumUser",
            _ => "Unknown"
        };
    }
}