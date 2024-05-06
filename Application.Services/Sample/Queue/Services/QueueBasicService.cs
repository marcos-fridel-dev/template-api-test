using Infrastructure.Services.Interfaces.Queue;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Sample.Queue.Services
{
    public class QueueBasicService(IQueueBasicService _queueBasic)
    {
        public async Task Send<T>(T request)
        {
            await _queueBasic.SendAsync("queue-basic", request);
        }

        public async Task<List<T>> Get<T>()
        {
            return await _queueBasic.GetAsync<T>("queue-basic");
        }

    }
}