using Azure.Storage.Queues;

namespace Way2GoApp.Services
{
    public class QueueService
    {
        private readonly QueueClient _queue;

        public QueueService(IConfiguration config)
        {
            var service = new QueueServiceClient(config.GetConnectionString("AzureStorage"));
            _queue = service.GetQueueClient("ordersqueue");
            _queue.CreateIfNotExists();
        }

        public async Task AddMessage(string msg) =>
            await _queue.SendMessageAsync(msg);

        public async Task<List<string>> GetMessages()
        {
            var msgs = await _queue.ReceiveMessagesAsync(5);
            return msgs.Value.Select(m => m.MessageText).ToList();
        }
    }
}