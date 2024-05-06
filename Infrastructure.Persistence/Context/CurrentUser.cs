using Infrastructure.Persistence.Enums.User;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Infrastructure.Persistence.Context
{
    public class CurrentUser(IHttpContextAccessor _httpContextAccessor) : ICurrentUser
    {
        private string FindFirstValue(Claims claim, string defaultValue)
        {
            try
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    return defaultValue;
                }
                return _httpContextAccessor.HttpContext.User.FindFirstValue(claim.ToString()) ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }
        private int FindFirstValue(Claims claim, int defaultValue)
        {
            int number = 0;
            try
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    return defaultValue;
                }

                if (int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(claim.ToString()), out number))
                    return number;
            }
            catch
            {
            }
            return defaultValue;
        }

        public Guid UserId => Guid.Parse(FindFirstValue(Claims.UserId, "00000000-0000-0000-0000-000000000000"));
        public string UserName => FindFirstValue(Claims.UserName, "system");
    }
}