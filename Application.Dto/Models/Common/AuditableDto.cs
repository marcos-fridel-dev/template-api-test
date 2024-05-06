using Application.Dto.Interfaces.Common;
using System;

namespace Application.Dto.Models.Common
{
    public class AuditableDto : UniqueDto, IAuditableDto
    {
        public DateTime Created { get; set; }
        public string CreatedLabel => Created.ToString("dd/MM/yyyy");
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? LastModified { get; set; }
        public string? LastModifiedLabel => LastModified?.ToString("dd/MM/yyyy");
        public string? LastModifiedBy { get; set; }
        public DateTime? Deleted { get; set; }
        public string? DeletedLabel => Deleted?.ToString("dd/MM/yyyy");
        public string? DeletedBy { get; set; }

    }
}