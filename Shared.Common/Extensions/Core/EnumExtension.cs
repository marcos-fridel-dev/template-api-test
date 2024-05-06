using System;
using System.ComponentModel;
using System.Reflection;

namespace Shared.Common.Extensions.Core
{
    public static class EnumExtension
    {

        //CREATE METHOD ENUM
        //https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-create-a-new-method-for-an-enumeration
        public static string Description<T>(this T source)
        {
            if (source == null) return "";

            FieldInfo? fi = source.GetType().GetField(source.ToString());

            if (fi == null) return "";

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            return (attributes.Length > 0 ? attributes[0].Description : source.ToString()) ?? "";
        }

        public static string? GetAttribute<T_Attribute>(this Enum source)
            where T_Attribute : Attribute
        {
            if (source == null) return "";

            FieldInfo? fi = source.GetType().GetField(source.ToString());

            if (fi == null) return "";

            T_Attribute[] attributes = (T_Attribute[])fi.GetCustomAttributes(
                typeof(T_Attribute), false);

            return (attributes.Length > 0 ? attributes[0].ToString() : source.ToString()) ?? "";
        }
    }
}