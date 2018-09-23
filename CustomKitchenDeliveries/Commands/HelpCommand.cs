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
            string path = Path.Combine(Environment.CurrentDirectory, $"CommandManuals/{commandName}.txt");
            using (StreamReader sr = new StreamReader(path))
            {
                response += sr.ReadToEnd();
            }
            response += "```";
            await commandData.RespondAsDM(response);
        }
    }
}
