using Application.Dto.Models.Location;
using Domain.Models.Location;

namespace Application.Dto.Mappings.Location
{
    public class HolidayProfile : DefaultProfile<Holiday, HolidayDto> { }
    public class HolidayCreateUpdateDtoProfile : DefaultProfile<Holiday, HolidayCreateUpdateDto> { }
}