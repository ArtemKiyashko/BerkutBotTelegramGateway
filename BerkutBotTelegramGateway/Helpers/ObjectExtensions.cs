using System;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace BerkutBotTelegramGateway.Helpers
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object update) => JsonConvert.SerializeObject(update);
    }
}

