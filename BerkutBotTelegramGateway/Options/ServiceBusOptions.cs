using System;
namespace BerkutBotTelegramGateway.Options
{
    public class ServiceBusOptions
    {
        public string MessageProcessorTopic { get; set; }
        public string ConnectionString { get; set; }
    }
}

