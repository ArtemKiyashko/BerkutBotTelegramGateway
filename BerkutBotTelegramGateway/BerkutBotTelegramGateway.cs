using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using BerkutBotTelegramGateway.Options;
using BerkutBotTelegramGateway.Infrastructure;
using Newtonsoft.Json;
using BerkutBotTelegramGateway.Helpers;

namespace BerkutBotTelegramGateway
{
    public class BerkutBotTelegramGateway
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly IServiceBusMessageFactory _serviceBusMessageFactory;
        private readonly ServiceBusOptions _options;

        public BerkutBotTelegramGateway(
            ServiceBusClient serviceBusClient,
            IOptions<ServiceBusOptions> options,
            IServiceBusMessageFactory serviceBusMessageFactory)
        {
            _serviceBusClient = serviceBusClient;
            _serviceBusMessageFactory = serviceBusMessageFactory;
            _options = options.Value;
        }

        [FunctionName("processTgMessage")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] Update incomingTgMessage,
            ILogger log)
        {
            string messageString = incomingTgMessage.ToJson();
            log.LogInformation($"Update received: {messageString}");

            try
            {
                await using ServiceBusSender sender = _serviceBusClient.CreateSender(_options.MessageProcessorTopic);
                ServiceBusMessage serviceBusMessage = _serviceBusMessageFactory.GetMessage(incomingTgMessage);
                await sender.SendMessageAsync(serviceBusMessage);

                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error sending to service bus: {0}", messageString);
                return new BadRequestObjectResult("Something went wrong, try again later");
            }
        }
    }
}

