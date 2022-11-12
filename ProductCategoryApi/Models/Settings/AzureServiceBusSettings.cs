namespace ProductCategoryApi.Models.Settings
{
    public class AzureServiceBusSettings
    {
        public string? ServiceBusConnectionString { get; init; }
        public string? CategoryTopic { get; init; }
        public string? SubcriptionName { get; init; }
    }
}
