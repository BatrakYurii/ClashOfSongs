using System.ComponentModel;
using System.Reflection;

namespace ClashOfMusic.Api.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum value)
        {
            var description = value.GetType().GetField(value.ToString());

            if (description == null)
                return null;

            return description.GetCustomAttribute<DescriptionAttribute>().Description;
        }
    }
}
