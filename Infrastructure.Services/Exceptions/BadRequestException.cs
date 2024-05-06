using Microsoft.Extensions.Localization;
using Language = Shared.Localization.Resources.Languages.Language;

namespace Infrastructure.Services.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(IStringLocalizer localizer) :
            base(localizer.GetString(Language.BadRequest))
        { }
        public BadRequestException(IStringLocalizer localizer, string text) :
            base(localizer.GetString(text))
        {
        }
    }
}