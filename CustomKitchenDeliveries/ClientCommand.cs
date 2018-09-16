using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Discord.WebSocket;

namespace CustomKitchenDeliveries
{
    class ClientCommand
    {
        public SocketMessage DiscordMessage { get; set; }
        public ISocketMessageChannel Channel { get; set; }
        public string Sender { get; private set; }
        public string Name { get; private set; }
        public List<string> Arguments { get; private set; }
        public bool Valid
        {
            get { return Name != null && Arguments.Count == ExpectedArguments; }
        }
        public int ExpectedArguments { get; set; }

        public ClientCommand(string command, SocketMessage message)
        {
            DiscordMessage = message;
            Channel = message.Channel;
            Sender = message.Author.Username;

            string fullCommand = Regex.Replace(command, "[ ]{2,}", " ");
            string[] commandSplit = fullCommand.Split(" ");
            Name = commandSplit?[0];

            if (commandSplit.Length > 1)
            {
                Arguments = commandSplit.ToList();
                Arguments.RemoveAt(0);
            }
            else
                Arguments = new List<string>();
        }
    }
}
