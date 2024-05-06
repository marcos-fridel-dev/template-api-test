using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Shared.Common.Extensions.Core
{
    public static class ObjectExtesion
    {

        public static T? GetValue<T>(this object obj, string nameProperty)
        {
            var value = obj.GetValue(nameProperty);
            return value == null ? default : (T)value;
        }

        public static object? GetValue(this object obj, string nameProperty)
        {
            var propertyInfo = obj.GetType().GetProperty(nameProperty);

            return propertyInfo?.GetValue(obj, null);
        }

        public static PropertyInfo[] GetProperties(this object obj)
        {
            var type = obj.GetType();

            return type.GetProperties();
        }

        public static string? GetDisplayName(this object obj)
        {
            return (obj
                .GetType()
                .GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .FirstOrDefault() as DisplayNameAttribute)?
                .DisplayName;
        }

        //public static string? GetDisplay(this object obj)
        //{
        //    return (obj
        //        .GetType()
        //        .GetCustomAttributes(typeof(DisplayAttribute), true)
        //        .FirstOrDefault() as DisplayAttribute)?
        //        .GetDisplay();
        //}

        public static string GetDisplayNameOrName(this object obj)
        {
            return obj.GetDisplayName() ?? obj.GetType().Name;
        }

        public static bool IsArray(this object obj)
        {
            return typeof(Array).IsAssignableFrom(obj.GetType());
        }

        public static bool IsInt(this object obj)
        {
            try
            {
                int value = int.Parse(obj.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int ToInt(this object obj)
        {
            return obj.IsInt() ? int.Parse(obj.ToString()) : 0;
        }

        public static List<T>? ToArray<T>(this object obj)
        {
            return obj.IsArray() ? obj as List<T> : new List<T>();
        }

        public static List<object>? ToArray(this object obj)
        {
            return obj.ToArray<object>();
        }

        public static void CopyTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            List<PropertyInfo> destinationProps = typeof(TDestination)
                .GetProperties()
                .Where(x => x.CanWrite)
                .ToList();

            List<PropertyInfo> sourceProps = typeof(TSource)
                .GetProperties()
                .Where(x => x.CanRead && destinationProps.Exists(d => d.Name == x.Name))
                .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destinationProps.Any(x => x.Name == sourceProp.Name))
                {
                    PropertyInfo property = destinationProps
                        .First(x => x.Name == sourceProp.Name);
                    if (property.CanWrite)
                    {
                        property.SetValue(destination, sourceProp.GetValue(source, null), null);
                    }
                }
            }
        }

        public static void CopyTo(this object source, object destination)
        {
            source.CopyTo<object, object>(destination);
        }
        public static T? GetEnum<T>(this object value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value.ToString());
        }
    }
}