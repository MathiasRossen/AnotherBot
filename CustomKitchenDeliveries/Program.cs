using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

namespace CustomKitchenDeliveries
{
    class Program
    {
        BotMain botController;

        static IMessageChannel debugChannel;

        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            ConfigHelper config = ApplicationController.Instance.ConfigHelper;
            DiscordSocketClient client = new DiscordSocketClient(config);

            // Bind methods to events
            client.Log += Log;
            client.MessageReceived += MessageRecieved;

            // Login and start
            await client.LoginAsync(TokenType.Bot, config.Token);
            await client.StartAsync();
            botController = new BotMain();

            // Makes sure the thread never dies
            await Task.Delay(-1);
        }

        private async Task MessageRecieved(SocketMessage message)
        {
            if(debugChannel == null)
                debugChannel = message.Channel;
            await botController.HandleMessage(message);
        }

        private Task Log(LogMessage logMessage)
        {
            Console.WriteLine("Log: {0}" ,logMessage.ToString());
            return Task.CompletedTask;
        }

        public static async Task DiscordDebug(string message)
        {
            await debugChannel.SendMessageAsync("`" + message + "`");
        }
    }
}
