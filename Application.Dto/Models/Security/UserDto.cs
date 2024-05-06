using Application.Dto.Interfaces.Common;
using Application.Dto.Models.Common;

namespace Application.Dto.Models.Security
{
    public class UserDto : UniqueDto, IUniqueDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}