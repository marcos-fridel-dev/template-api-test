using Domain.Interfaces.Common;
using Domain.Models.Common;
using System.Collections.Generic;

namespace Domain.Models.Security
{
    public class Role : Unique, IUnique
    {
        public string Name { get; set; }
        public List<User> Users { get; } = [];
    }
}