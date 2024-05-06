using System;

namespace Domain.Interfaces.Common
{
    public interface IUnique
    {
        public Guid Id { get; set; }
        //public Guid id { get; set; }
        public bool IsDeleted { get; set; }
    }
}