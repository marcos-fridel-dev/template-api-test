using Microsoft.Extensions.Localization;
using Shared.Common.Enums.Core;

namespace Shared.Common.Extensions.Core
{
    public static class BoolExtension
    {
        public static string ToYesNo(this bool value) =>
            value ? YesNo.Yes.Description() : YesNo.No.Description();
        public static string ToYesNo(this bool? value) =>
            value ?? false ? YesNo.Yes.Description() : YesNo.No.Description();
        public static string ToYesNo(this bool value, IStringLocalizer localizer) =>
            localizer.GetString(value ? YesNo.Yes.Description() : YesNo.No.Description());
        public static string ToYesNo(this bool? value, IStringLocalizer localizer) =>
            localizer.GetString(value ?? false ? YesNo.Yes.Description() : YesNo.No.Description());
    }
}