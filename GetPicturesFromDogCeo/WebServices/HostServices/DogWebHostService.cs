using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using GetPicturesFromDogCeo.Interfaces.WebServices;
using GetPicturesFromDogCeo.ViewModels;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GetPicturesFromDogCeo.WebServices.HostServices
{
    public class DogWebHostService : BackgroundService
    {
        private readonly ILogger<DogWebHostService> _loger;
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<object, bool> _sessionsRun;
        private readonly SemaphoreSlim _semaphore;

        private event Action<(DogsQueryViewModel QueryVm, object SessionId)> _executeAsyncNotify;

        public DogWebHostService(ILogger<DogWebHostService> loger, IServiceProvider serviceProvider)
        {
            _loger = loger;
            _serviceProvider = serviceProvider;
            _semaphore = new SemaphoreSlim(5);
            _sessionsRun = new Dictionary<object, bool>();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _executeAsyncNotify += async (x) =>
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetService<IDogWebService>();
                        await _semaphore.WaitAsync();
                        await service.GetDogsAsync(x.QueryVm.Count, new CancellationTokenSource().Token, x.QueryVm.Breads);
                        _semaphore.Release();
                        if (x.SessionId != null && _sessionsRun.ContainsKey(x.SessionId))
                            _sessionsRun.Remove(x.SessionId);
                    }
                };

            }
            catch (Exception ex)
            {
                _loger.LogError($"{ex.Message} {ex.StackTrace} {ex.InnerException}");
            }

            return Task.CompletedTask;
        }

        public bool StartEventExecute(DogsQueryViewModel dogsQueryViewModel, object sessionId = null)
        {
            sessionId ??= "default";

            if (_sessionsRun.ContainsKey(sessionId))
                return false;
            _executeAsyncNotify?.Invoke((dogsQueryViewModel, sessionId));
            _sessionsRun.Add(sessionId, true);
            return true;
        }
    }
}
