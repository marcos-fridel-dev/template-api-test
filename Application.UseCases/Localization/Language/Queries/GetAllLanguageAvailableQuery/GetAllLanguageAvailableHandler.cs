using Application.Dto.Models.Localization;
using AutoMapper;
using MediatR;
using Shared.Localization.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Localization.Language.Queries.GetAllLanguageAvailableQuery
{
    public class GetAllLanguageAvailableHandler(IMapper _mapper) : IRequestHandler<GetAllLanguageAvailableUseCase, IEnumerable<LanguageDto>>
    {
        public async Task<IEnumerable<LanguageDto>> Handle(GetAllLanguageAvailableUseCase request, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<LanguageDto>>(
                LanguageResource.GetAvailableLanguages())
                .OrderBy(x => x.Name);
        }
    }
}