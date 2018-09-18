using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CustomKitchenDeliveries.Commands
{
    class HelpCommand : BaseCommand
    {
        public override bool NeedMod => false;
        public override int ExpectedArguments => 0;

        public override async Task Execute(ClientCommand commandData)
        {
            string commandName = "commands";
            if(commandData.Count > 0)
                commandName = commandData.Arguments[0];
            string response = "```";
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, $"CommandManuals\\{commandName}.txt")))
            {
                response += sr.ReadToEnd();
            }
            response += "```";
            await commandData.Respond(response);
        }
    }
}
