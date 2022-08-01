using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using BerkutBotTelegramGateway.Helpers;
using Telegram.Bot.Types;

namespace BerkutBotTelegramGateway.Infrastructure
{
    public class ServiceBusMessageFactory : IServiceBusMessageFactory
    {
        public ServiceBusMessage GetMessage(Message message)
        {
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message.ToJson());
            serviceBusMessage.SessionId = message.Chat.Id.ToString();

            return serviceBusMessage;
        }
    }
}

