using Domain.Interfaces.Common;
using System;

namespace Domain.Models.Common
{
    public class Unique : IUnique
    {
        public Guid Id { get; set; }
        //public Guid id { get; set; }
        public bool IsDeleted { get; set; }
    }
}