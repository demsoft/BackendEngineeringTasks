using BackendEngineeringTasks.Application.Services;

namespace BackendEngineeringTasks.Infrastructure.Services
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly ITaskAppService _taskAppService;
        private readonly ILogger<NotificationBackgroundService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1440);  //It will run once in a day with 1440 minutes
        public NotificationBackgroundService(ILogger<NotificationBackgroundService> logger, IServiceScopeFactory _factory)
        {
            _taskAppService = _factory.CreateScope().ServiceProvider.GetRequiredService<ITaskAppService>();
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Call the method to send notifications for due tasks
                    await _taskAppService.SendNotificationsForDueTasksAsync();

                    // Log that the task has been executed successfully
                    _logger.LogInformation("Notification background service executed successfully.");

                    // Sleep for the specified interval before the next execution
                    await Task.Delay(_interval, stoppingToken);
                }
                catch (Exception ex)
                {
                    // Handle exceptions and log errors
                    _logger.LogError(ex, "An error occurred while executing the notification background service.");
                }
            }
            _logger.LogInformation("Background service stopped.");
        }
    }
}
