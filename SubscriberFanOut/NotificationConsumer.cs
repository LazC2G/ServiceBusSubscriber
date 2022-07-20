using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EventBus
{
    public record Notification
    {
        public string Text { get; set; }
    }

    public class NotificationConsumer :
        IConsumer<Notification>
    {
        readonly ILogger<NotificationConsumer> _logger;

        public NotificationConsumer(ILogger<NotificationConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Notification> context)
        {
            _logger.LogInformation("Notification Consumer: {Text}", context.Message.Text);

            return Task.CompletedTask;
        }
    }
}