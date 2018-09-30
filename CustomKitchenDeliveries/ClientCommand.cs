using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace CustomKitchenDeliveries
{
    public class ClientCommand
    {
        public SocketMessage DiscordMessage { get; set; }
        public ISocketMessageChannel Channel { get; set; }
        public SocketUser Sender { get; private set; }
        public string Name { get; private set; }
        public List<string> Arguments { get; private set; }
        public int Count => Arguments.Count;
        public string SenderIdString => Sender.Id.ToString();

        public ClientCommand(string command, SocketMessage message)
        {
            DiscordMessage = message;
            Channel = message.Channel;
            Sender = message.Author;

            string fullCommand = Regex.Replace(command, "[ ]{2,}", " ");
            string[] commandSplit = Regex.Split(fullCommand, " \"([A-Za-z \\d\\-]+)\" |\\s");
            Name = commandSplit?[0];

            if (commandSplit.Length > 1)
            {
                Arguments = commandSplit.ToList();
                Arguments.RemoveAt(0);

                for(int i = 0; i < Arguments.Count; i++)
                {
                    Arguments[i] = Arguments[i].Trim();
                }
            }
            else
                Arguments = new List<string>();
        }

        public async Task Respond(string message)
        {
            await Channel.SendMessageAsync(message);
        }

        public async Task RespondAsDM(string message)
        {
            await Sender.GetOrCreateDMChannelAsync().Result.SendMessageAsync(message);
        }
    }
}
