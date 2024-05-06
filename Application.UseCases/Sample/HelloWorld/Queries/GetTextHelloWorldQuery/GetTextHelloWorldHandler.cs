using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Localization.Resources.Languages;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Sample.HelloWorld.Queries.GetTextHelloWorldQuery
{
    public class GetTextHelloWorldHandler(IStringLocalizer localizer) : IRequestHandler<GetTextHelloWorldUseCase, string>
    {
        public async Task<string> Handle(GetTextHelloWorldUseCase request, CancellationToken cancellationToken = default)
        {
            return localizer.GetString(Language.HelloWorld);
        }
    }
}