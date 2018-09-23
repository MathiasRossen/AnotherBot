using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace CustomKitchenDeliveries
{
    class Program
    {
        BotMain botMain;

        static void Main(string[] args)
        {
            Console.Title = "Mhbot";
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            BotConfig config = ApplicationController.Instance.BotConfig;
            DiscordSocketClient client = new DiscordSocketClient(config);

            // Bind methods to events
            client.Log += Log;
            client.MessageReceived += MessageRecieved;

            // Login and start
            await client.LoginAsync(TokenType.Bot, config.Token);
            await client.StartAsync();
            botMain = new BotMain();

            // Makes sure the thread never dies
            await Task.Delay(-1);
        }

        private async Task MessageRecieved(SocketMessage message)
        {
            await botMain.HandleMessage(message);
        }

        private Task Log(LogMessage logMessage)
        {
            Console.WriteLine("Log: {0}" ,logMessage.ToString());
            return Task.CompletedTask;
        }
    }
}
