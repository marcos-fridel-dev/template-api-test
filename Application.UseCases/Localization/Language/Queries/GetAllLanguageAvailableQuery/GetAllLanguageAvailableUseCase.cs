using Application.Dto.Models.Localization;
using MediatR;
using System.Collections.Generic;

namespace Application.UseCases.Localization.Language.Queries.GetAllLanguageAvailableQuery
{
    public sealed class GetAllLanguageAvailableUseCase : IRequest<IEnumerable<LanguageDto>>
    {
    }
}