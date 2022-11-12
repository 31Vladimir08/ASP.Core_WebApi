using System.Text;
using System.Threading;

using AutoMapper;

using Azure.Messaging.ServiceBus;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using ProductCategoryApi.EntityDto;
using ProductCategoryApi.Interfaces.Repositories;
using ProductCategoryApi.Interfaces.Services;
using ProductCategoryApi.Models;
using ProductCategoryApi.Models.Settings;
using ProductCategoryApi.Repositories;

namespace ProductCategoryApi.Messaging
{
    public class AzureServiceBusConsumerHostedService : IHostedService
    {
        private readonly IMapper _mapper;
        private readonly AzureServiceBusSettings _options;
        private readonly IServiceProvider _serviceProvider;

        private ServiceBusProcessor _serviceBusProcessor;

        public AzureServiceBusConsumerHostedService(
            IOptions<AzureServiceBusSettings> options, 
            IMapper mapper,
            IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _options = options.Value;
            _serviceProvider = serviceProvider;

            var client = new ServiceBusClient(_options.ServiceBusConnectionString);
            _serviceBusProcessor = client.CreateProcessor(_options.CategoryTopic, _options.SubcriptionName);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _serviceBusProcessor.ProcessMessageAsync += CreateCategoryMessageReceived;
            _serviceBusProcessor.ProcessErrorAsync += ErrorHandler;

            await _serviceBusProcessor.StartProcessingAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            throw new NotImplementedException();
        }

        private async Task CreateCategoryMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            var categoryDto = JsonConvert.DeserializeObject<CategoryDto>(body);

            //var category = _mapper.Map<Category>(categoryDto);
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<ICategoryService>();
                await service.CreateCategoryAsync(categoryDto);
            }            
        }
    }
}
