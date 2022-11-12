using MessageBus.Models;

namespace GatewayWebApi.ModelsDto
{
    public class CategoryDto : BaseMessage
    {
        public string? CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string? Description { get; set; }
    }
}
