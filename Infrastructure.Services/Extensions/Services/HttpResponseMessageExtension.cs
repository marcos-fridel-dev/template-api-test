using Infrastructure.Services.Exceptions;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Services.Extensions.Services
{
    public static class HttpResponseMessageExtension
    {
        public static HttpResponseMessage ValidateHttpStatusCodeAndThrowException(this HttpResponseMessage response, IStringLocalizer localizer)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpResponseMessageException(localizer);
            };
            return response;
        }
    }
}