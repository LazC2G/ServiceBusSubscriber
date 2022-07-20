using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EventBus
{
    public record NotificationFanOut
    {
        public string Text { get; set; }
    }

    public class NotificationFanOutConsumer :
        IConsumer<NotificationFanOut>
    {
        readonly ILogger<NotificationFanOutConsumer> _logger;

        public NotificationFanOutConsumer(ILogger<NotificationFanOutConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<NotificationFanOut> context)
        {
            _logger.LogInformation("NotificationFanOut Consumer Never reached!");

            return Task.CompletedTask;
        }
    }
}