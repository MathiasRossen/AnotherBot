using CustomKitchenDeliveries.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    class AddModCommand : BaseCommand
    {
        public override bool NeedMod => true;
        public override int ExpectedArguments => 1;

        public override async Task Execute(ClientCommand commandData)
        {
            string discordIdText = commandData.Arguments[0];
            if (!ulong.TryParse(discordIdText, out ulong discordId))
                await commandData.RespondAsDM("Invalid Id");
            else
            {
                application.AddMod(discordId);
                await commandData.RespondAsDM($"{discordId} was added to the mod list");
            }
        }
    }
}
