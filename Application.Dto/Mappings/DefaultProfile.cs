using AutoMapper;

namespace Application.Dto.Mappings
{
    public class DefaultProfile<TEntity, TDto> : Profile
    {
        public DefaultProfile()
        {
            CreateMap<TEntity, TDto>().ReverseMap();
        }
    }
}