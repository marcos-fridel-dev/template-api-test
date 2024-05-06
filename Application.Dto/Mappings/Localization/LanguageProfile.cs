using Application.Dto.Models.Localization;
using Shared.Common.Extensions.Core;
using System.Globalization;

namespace Application.Dto.Mappings.Localization
{
    public class LanguageProfile : DefaultProfile<CultureInfo, LanguageDto>
    {
        public LanguageProfile()
        {
            CreateMap<CultureInfo, LanguageDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.NativeName.ToCamelCase()))
                .ReverseMap();
        }
    }
}