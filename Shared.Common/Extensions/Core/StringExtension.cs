using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Shared.Common.Extensions.Core
{
    public static class StringExtension
    {

        public static bool IsNullOrWhiteSpace(this string? value)
        {
            if (value == null) return true;

            if (value.Trim().Length == 0) return true;

            return false;
        }

        public static string Right(this string input, int count) =>
            count > input.Length ? "" : input.Substring(Math.Max(input.Length - count, 0), Math.Min(count, input.Length));

        public static string Left(this string input, int count) =>
            count > input.Length ? "" : input.Substring(0, count);

        public static bool IsJson(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) { return false; }

            input = input.Trim();

            if (input.StartsWith("{") && input.EndsWith("}") || //For object
                input.StartsWith("[") && input.EndsWith("]")) //For array
            {
                try
                {
                    var obj = JToken.Parse(input);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static string ToCamelCase(this string str)
        {
            return
                string.IsNullOrEmpty(str) || str.Length < 2
                ? str.ToUpperInvariant()
                : char.ToUpperInvariant(str[0]) + str.Substring(1);
        }
    }
}