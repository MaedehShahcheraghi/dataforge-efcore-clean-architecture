using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DataForge.Domain.Common.Extensions;

public static partial class EnumExtensions
{
    public static string ToErrorCodeString(this Enum value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var field = value
            .GetType()
            .GetField(value.ToString());

        var description = field?
            .GetCustomAttribute<DescriptionAttribute>()?
            .Description;

        if (!string.IsNullOrWhiteSpace(description))
        {
            return description;
        }

        return SnakeCaseRegex()
            .Replace(value.ToString(), "_$1")
            .ToLowerInvariant();
    }

    [GeneratedRegex(@"(?<!^)([A-Z])")]
    private static partial Regex SnakeCaseRegex();
}
