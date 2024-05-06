using System;

namespace Application.Dto.Interfaces.Common
{
    public interface IAuditableDto : IUniqueDto
    {
        DateTime Created { get; set; }
        string CreatedLabel { get; }
        string CreatedBy { get; set; }
        DateTime? LastModified { get; set; }
        string? LastModifiedLabel { get; }
        string? LastModifiedBy { get; set; }
        DateTime? Deleted { get; set; }
        string? DeletedLabel { get; }
        string? DeletedBy { get; set; }
    }
}