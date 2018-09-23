using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    class AmIModCommand : BaseCommand
    {
        public override bool NeedMod => false;
        public override int ExpectedArguments => 0;

        public override async Task Execute(ClientCommand commandData)
        {
            await commandData.RespondAsDM(CommandValidator.IsMod(commandData.Sender).ToString());
        }
    }
}
