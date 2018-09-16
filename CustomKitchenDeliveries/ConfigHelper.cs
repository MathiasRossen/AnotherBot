using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomKitchenDeliveries
{
    class ConfigHelper : DiscordSocketConfig
    {
        IConfigurationRoot config;

        public string Token
        {
            get { return config["BotToken"]; }
        }

        public ConfigHelper()
            :base()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("DiscordConfig.json")
                .Build();
        }
    }
}
