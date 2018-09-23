using CustomKitchenDeliveries.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    class RemoveChallengeCommand : BaseCommand
    {
        public override bool NeedMod => true;
        public override int ExpectedArguments => 1;

        public override async Task Execute(ClientCommand commandData)
        {
            string identifier = commandData.Arguments[0];
            Challenge c = application.Challenges.Find(x => x.Identifier == identifier);
            if(c == null)
                await commandData.Respond($"Challenge with identifier \"{identifier}\" does not exist {Emotes.ERROR}");
            else
            {
                application.RemoveChallenge(identifier);
                await commandData.Respond($"`{c.Name} ({c.Identifier})`{Emotes.WeaponsArray[(int)c.Weapon]} was removed");
            }
        }
    }
}
