using Application.Dto.Interfaces.Common;
using Application.Dto.Models.Common;

namespace Application.Dto.Models.Security
{
    public class RoleDto : UniqueDto, IUniqueDto
    {
        public string Name { get; set; }
    }
}