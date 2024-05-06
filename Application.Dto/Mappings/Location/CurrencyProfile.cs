using Application.Dto.Models.Location;
using Domain.Models.Location;

namespace Application.Dto.Mappings.Location
{
    public class CurrencyProfile : DefaultProfile<Currency, CurrencyDto> { }
    public class CurrencyPostDtoProfile : DefaultProfile<Currency, CurrencyPostDto> { }
    public class CurrencyUpdateProfile : DefaultProfile<Currency, CurrencyUpdateDto> { }
    public class CurrencyPostUpdateDtoProfile : DefaultProfile<CurrencyPostDto, CurrencyUpdateDto> { }
}