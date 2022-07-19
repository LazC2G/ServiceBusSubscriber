using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EventBus
{
    public record JSON
    {
        public string Text { get; set; }
    }

    public class JSONConsumer :
        IConsumer<JSON>
    {
        readonly ILogger<JSONConsumer> _logger;

        public JSONConsumer(ILogger<JSONConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<JSON> context)
        {
            _logger.LogInformation("Received Text: {Text}", context.Message.Text);

            return Task.CompletedTask;
        }
    }
}