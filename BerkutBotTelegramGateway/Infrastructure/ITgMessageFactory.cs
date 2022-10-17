using System;
using Telegram.Bot.Types;

namespace BerkutBotTelegramGateway.Infrastructure
{
    public interface ITgMessageFactory
    {
        public Message GetMessage(Update incomingUpdate);
        public Chat GetChat(Update incomingUpdate);
    }
}

