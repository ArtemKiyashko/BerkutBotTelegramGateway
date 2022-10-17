using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using BerkutBotTelegramGateway.Helpers;
using Telegram.Bot.Types;

namespace BerkutBotTelegramGateway.Infrastructure
{
    public class ServiceBusMessageFactory : IServiceBusMessageFactory
    {
        private readonly ITgMessageFactory _tgMessageFactory;

        public ServiceBusMessageFactory(ITgMessageFactory tgMessageFactory)
        {
            _tgMessageFactory = tgMessageFactory;
        }

        public ServiceBusMessage GetMessage(Update update)
        {
            var tgChat = _tgMessageFactory.GetChat(update);
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(update.ToJson());
            serviceBusMessage.SessionId = tgChat.Id.ToString();

            return serviceBusMessage;
        }
    }
}

