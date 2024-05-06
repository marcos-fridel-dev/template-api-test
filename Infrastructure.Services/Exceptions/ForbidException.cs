using Microsoft.Extensions.Localization;
using Shared.Localization.Resources.Languages;

namespace Infrastructure.Services.Exceptions
{
    public class ForbidException : Exception
    {
        public ForbidException(IStringLocalizer localizer) :
            base(localizer.GetString(Language.Forbid))
        { }

        public ForbidException(IStringLocalizer localizer, string text) :
            base(localizer.GetString(text))
        {
        }
    }
}