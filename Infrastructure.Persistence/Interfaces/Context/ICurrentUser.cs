using System;

namespace Infrastructure.Persistence.Interfaces.Context
{
    public interface ICurrentUser
    {
        public Guid UserId { get; }
        public string UserName { get; }
    }
}