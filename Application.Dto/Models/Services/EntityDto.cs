using Application.Dto.Interfaces.Common;
using Application.Dto.Models.Common;

namespace Application.Dto.Models.Sample
{
    public class EntityDto : AuditableDto, IAuditableDto
    {
        public string Name { get; set; }
    }

    public class EntityPostDto
    {
        public string Name { get; set; }
    }

    public class EntityUpdateDto : EntityPostDto, IIdentityDto
    {
        public Guid Id { get; set; }
    }

}
