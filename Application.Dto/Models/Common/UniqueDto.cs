using Application.Dto.Interfaces.Common;
using Shared.Common.Extensions.Core;
using System;

namespace Application.Dto.Models.Common
{
    public class UniqueDto : IUniqueDto
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string IsDeletedLabel => IsDeleted.ToYesNo();
    }
}