using MediatR;

namespace Application.UseCases.Sample.HelloWorld.Queries.GetTextHelloWorldQuery
{
    public sealed class GetTextHelloWorldUseCase : IRequest<string>
    {
    }
}