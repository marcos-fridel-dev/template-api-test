using Domain.Interfaces.Common;
using Domain.Models.Common;
using Domain.Models.Security;
using System;

namespace Domain.Models.Authentication
{
    public class Login : Auditable, IAuditable
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string Password { get; set; }
        public bool IsCurrent { get; set; }
    }
}