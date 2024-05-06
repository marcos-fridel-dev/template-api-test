using Domain.Enums.Security;
using Domain.Interfaces.Common;
using Domain.Models.Common;
using System.Collections.Generic;

namespace Domain.Models.Security
{
    public class User : Unique, IUnique
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
        public List<Role> Roles { get; set; } = [];
    }
}