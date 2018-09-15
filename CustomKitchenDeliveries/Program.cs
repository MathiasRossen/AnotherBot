using System;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace CustomKitchenDeliveries
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            DiscordSocketClient client = new DiscordSocketClient();

            // Makes sure the thread never dies
            await Task.Delay(-1);
        }
    }
}
