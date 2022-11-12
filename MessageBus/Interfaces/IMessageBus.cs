using MessageBus.Models;

namespace MessageBus.Interfaces
{
    public interface IMessageBus
    {
        Task PublishMessageAsync(BaseMessage message, string topicName);
    }
}
