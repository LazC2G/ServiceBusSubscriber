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
            var uniqueId = DateTimeOffset.Now.Millisecond.ToString();
            Console.WriteLine(uniqueId.ToString());
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();
                     
                        x.AddConsumer<NotificationFanOutConsumer>()
                        .Endpoint(e => { e.InstanceId = uniqueId; });
                        x.AddConsumer<NotificationConsumer>()
                        .Endpoint(e => { e.InstanceId = uniqueId; });
                      
                        x.UsingAzureServiceBus((context, cfg) =>
                        {
                            cfg.Host("Endpoint=sb://");

                            cfg.SubscriptionEndpoint<NotificationFanOut>("notification-fan-out-consumer", e =>
                            {
                                e.ConfigureConsumer<NotificationFanOutConsumer>(context);
                            });

                            cfg.ConfigureEndpoints(context);
                        });
                    });

                  
                }).Build().RunAsync();
        }
    }
}
