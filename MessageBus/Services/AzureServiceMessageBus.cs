using System.Text;

using Azure.Messaging.ServiceBus;

using MessageBus.Interfaces;
using MessageBus.Models;
using Newtonsoft.Json;

namespace MessageBus.Services
{
    public class AzureServiceMessageBus : IMessageBus
    {
        private readonly string _connectionString;

        public AzureServiceMessageBus(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task PublishMessageAsync(BaseMessage message, string topicName)
        {
            await using (var client = new ServiceBusClient(_connectionString))
            {
                var sender = client.CreateSender(topicName);
                var jsonMessage = JsonConvert.SerializeObject(message);
                var publisMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
                {
                    CorrelationId = Guid.NewGuid().ToString(),
                };

                await sender.SendMessageAsync(publisMessage);
                await client.DisposeAsync();
            }   
        }
    }
}
