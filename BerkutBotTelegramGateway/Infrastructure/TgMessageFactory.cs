using System;
using Telegram.Bot.Types;

namespace BerkutBotTelegramGateway.Infrastructure
{
    public class TgMessageFactory : ITgMessageFactory
    {
        public Chat GetChat(Update incomingUpdate) => incomingUpdate?.Type switch
        {
            Telegram.Bot.Types.Enums.UpdateType.Message => incomingUpdate.Message.Chat,
            Telegram.Bot.Types.Enums.UpdateType.EditedMessage => incomingUpdate.EditedMessage.Chat,
            Telegram.Bot.Types.Enums.UpdateType.MyChatMember => incomingUpdate.MyChatMember.Chat,
            _ => null
        };

        public Message GetMessage(Update incomingUpdate) => incomingUpdate?.Type switch
        {
            Telegram.Bot.Types.Enums.UpdateType.Message => incomingUpdate.Message,
            Telegram.Bot.Types.Enums.UpdateType.EditedMessage => incomingUpdate.EditedMessage,
            _ => null
        };
    }
}

