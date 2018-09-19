using CustomKitchenDeliveries.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    class ListChallengesCommand : BaseCommand
    {
        public override bool NeedMod => false;
        public override int ExpectedArguments => 0;

        public override async Task Execute(ClientCommand commandData)
        {
            IList<Challenge> challenges = application.Challenges;
            if (challenges.Count <= 0)
                await commandData.Respond($"There are no challenges {Emotes.ERROR}");
            else
            {
                string list = "";
                foreach (Challenge c in challenges)
                {
                    Player creator = application.Players.Find(x => x.DiscordId == c.PlayerDiscordId);
                    list += $"```{c.Name} ({c.Identifier})\nWeapon: {c.Weapon.ToString()}, Posted by: {creator.Name}\n\n```";
                }
                await commandData.Respond(list);
            }
        }
    }
}
