namespace Taxually.TechnicalTest
{
    public interface ITaxuallyQueueClient
    {
        public Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
    }

    public class TaxuallyQueueClient : ITaxuallyQueueClient
    {
        public Task EnqueueAsync<TPayload>(string queueName, TPayload payload)
        {
            // Code to send to message queue removed for brevity
            return Task.CompletedTask;
        }
    }
}
