using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Telegram.Bot.Types;

namespace BerkutBotTelegramGateway.Infrastructure
{
    public interface IServiceBusMessageFactory
    {
        public ServiceBusMessage GetMessage(Update update);
    }
}

