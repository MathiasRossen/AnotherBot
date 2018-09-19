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

        public string Token => config["BotToken"];
        public string Database => config["Database"];
        public new int MessageCacheSize => int.Parse(config["MessageCacheSize"]);

        public ConfigHelper()
            : base()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("DiscordConfig.json")
                .Build();
        }
    }
}
