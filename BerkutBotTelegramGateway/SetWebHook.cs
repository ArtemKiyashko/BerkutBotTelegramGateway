using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BerkutBotTelegramGateway.Options;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace BerkutBotTelegramGateway
{
    public class SetWebHook
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly BotOptions _options;

        public SetWebHook(ITelegramBotClient telegramBotClient, IOptions<BotOptions> options)
        {
            _telegramBotClient = telegramBotClient;
            _options = options.Value;
        }

        [FunctionName("SetWebHook")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Setting new webhook");

            var dropPendingUpdatesParameter = req.Query["dropPendingUpdates"].Count > 0 ? req.Query["dropPendingUpdates"][0] : "False";

            bool.TryParse(dropPendingUpdatesParameter, out bool dropPendingUpdates);

            try
            {
                await _telegramBotClient.SetWebhookAsync(
                    url: _options.WebHookUrl.ToString(),
                    dropPendingUpdates: dropPendingUpdates);
            }
            catch (Exception ex)
            {
                log.LogError("Error setting webhook", ex);
                return new BadRequestObjectResult("Error setting webhook");
            }

            return new OkObjectResult($"Webhook set. Drop pending updates: {dropPendingUpdates}");
        }
    }
}

