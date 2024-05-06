//using Application.Services.Sample.Subscription.Models;
//using Application.Services.Sample.Subscription.Services;
//using MediatR;

//namespace Application.UseCases.Sample.Subscription.Commands
//{
//    public class QueueBasicSuscriptionUseCase(QueueBasicSampleEvent _request) : IRequest
//    {
//        public QueueBasicSampleEvent Request => _request;

//        public class Handler(QueueBasicSubscriptionService _subscriptionService) : IRequestHandler<QueueBasicSuscriptionUseCase>
//        {

//            public async Task Handle(QueueBasicSuscriptionUseCase request, CancellationToken cancellationToken = default)
//            {
//               await _subscriptionService.Publish(request.Request);
//            }
//        }
//    }
//}
