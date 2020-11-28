namespace ClimaCellCore.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    public static class EnumExtensions
    {
        public static string GetMemberValue(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<EnumMemberAttribute>()
                            .Value;
        }
    }
}