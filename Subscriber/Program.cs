using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;
using System;
using System.Threading.Tasks;


namespace EventBus
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<MessageConsumer>();
                        //x.AddConsumer<JSONConsumer>();
                        IBusRegistrationContext thing;
                        x.UsingAzureServiceBus((context, cfg) =>
                        {
                            cfg.SubscriptionEndpoint<Message>("message", e =>
                            {
                                e.ConfigureConsumer<MessageConsumer>(context);
                            });
                            cfg.Host("Endpoint=sb://");
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                }).Build().RunAsync();
        }
    }
}
