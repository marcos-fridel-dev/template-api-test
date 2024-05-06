using Microsoft.Extensions.Localization;
using Shared.Localization.Resources.Languages;

namespace Infrastructure.Services.Exceptions
{
    public class HttpResponseMessageException : Exception
    {
        public HttpResponseMessageException(IStringLocalizer localizer) :
            base(localizer.GetString(Language.RequestToTheServiceCouldNotBeProcessed))
        { }
    }
}