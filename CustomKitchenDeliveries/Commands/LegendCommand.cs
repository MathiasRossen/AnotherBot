using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    class LegendCommand : BaseCommand
    {
        public override bool NeedMod => false;
        public override int ExpectedArguments => 0;

        public override async Task Execute(ClientCommand commandData)
        {
            string legend =
                $"{Emotes.SNS} `Sword and Shield`\n" +
                $"{Emotes.DB} `Dual Blades`\n" +
                $"{Emotes.LS} `Long Sword`\n" +
                $"{Emotes.GS} `Great Sword`\n" +
                $"{Emotes.HA} `Hammer`\n" +
                $"{Emotes.HH} `Hunting Horn`\n" +
                $"{Emotes.SA} `Switch Axe`\n" +
                $"{Emotes.CB} `Charge Blade`\n" +
                $"{Emotes.LA} `Lance`\n" +
                $"{Emotes.GL} `Gunlance`\n" +
                $"{Emotes.LBG} `Light Bowgun`\n" +
                $"{Emotes.HBG} `Heavy Bowgun`\n" +
                $"{Emotes.IG} `Insect Glaive`\n" +
                $"{Emotes.BOW} `Bow`";
            await commandData.Respond(legend);
        }
    }
}
