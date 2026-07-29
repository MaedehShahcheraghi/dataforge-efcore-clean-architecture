using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DataForge.Domain.Common.Extensions
{
    public static partial class EnumExtensions
    {
        public static string ToErrorCodeString(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            if (attribute != null)
            {
                return attribute.Description;
            }

            var name = value.ToString();
            var snakeCase = SnakeCaseRegex().Replace(name, "_$0");

            return snakeCase.ToUpperInvariant();
        }

        [GeneratedRegex(@"(?<=.)([A-Z])")]
        private static partial Regex SnakeCaseRegex();
    }
}
