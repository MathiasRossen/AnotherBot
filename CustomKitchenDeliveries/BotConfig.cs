using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CustomKitchenDeliveries
{
    class BotConfig : DiscordSocketConfig
    {
        IConfigurationRoot config;

        public string Token => config["BotToken"];
        public string Database => config["Database"];
        public new int MessageCacheSize => int.Parse(config["MessageCacheSize"]);
        public string ApplicationPath => Environment.CurrentDirectory + "/";
        public string ImagePath => ApplicationPath + config["ImageFolder"] + "/";

        public BotConfig()
            : base()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("DiscordConfig.json")
                .Build();
            Directory.CreateDirectory(ImagePath);
        }
    }
}
