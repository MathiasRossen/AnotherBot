using CustomKitchenDeliveries.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    class RemoveScoreCommand : BaseCommand
    {
        public override bool NeedMod => true;
        public override int ExpectedArguments => 2;

        public override async Task Execute(ClientCommand commandData)
        {
            string identifier = commandData.Arguments[0];
            string username = commandData.Arguments[1];

            Challenge c = application.Challenges.Find(x => x.Identifier == identifier);
            Player p = application.Players.Find(x => x.Name == username);
            Score s = application.Scores.Find(x => x.ChallengeId == c?.Id && x.PlayerDiscordId == p?.DiscordId);

            if (s == null)
                await commandData.Respond($"Either the challenge does not exist or the player is not on that challenge {Emotes.ERROR}");
            else
            {
                application.RemoveScore(s);
                await commandData.Respond($"{p.Name} was removed from `{c.Name} ({c.Identifier})`{Emotes.WeaponsArray[(int)c.Weapon]}");
            }
        }
    }
}
