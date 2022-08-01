using System;
using Azure.Identity;
using BerkutBotTelegramGateway.Infrastructure;
using BerkutBotTelegramGateway.Options;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;

[assembly: FunctionsStartup(typeof(BerkutBotTelegramGateway.Startup))]

namespace BerkutBotTelegramGateway
{
    public class Startup : FunctionsStartup
    {
        private IConfigurationRoot _functionConfig;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            _functionConfig = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddLogging();

            builder.Services.Configure<BotOptions>(_functionConfig.GetSection("BotOptions"));
            builder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider =>
                new TelegramBotClient(provider.GetService<IOptions<BotOptions>>().Value.Token));
            builder.Services.AddTransient<ITgMessageFactory, TgMessageFactory>();
            builder.Services.AddTransient<IServiceBusMessageFactory, ServiceBusMessageFactory>();

            builder.Services.AddAzureClients(clientBuilder =>
            {
                builder.Services.Configure<ServiceBusOptions>(_functionConfig.GetSection("ServiceBusOptions"));
                var provider = builder.Services.BuildServiceProvider();

                clientBuilder.AddServiceBusClient(provider.GetRequiredService<IOptions<ServiceBusOptions>>().Value.ConnectionString);
            });
        }
    }
}

