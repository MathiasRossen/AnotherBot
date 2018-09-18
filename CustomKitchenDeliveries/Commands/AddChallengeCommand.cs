using CustomKitchenDeliveries.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    class AddChallengeCommand : BaseCommand
    {
        public override bool NeedMod => false;
        public override int ExpectedArguments => 2;

        public override async Task Execute(ClientCommand commandData)
        {
            Player p = application.Players.Find(x => x.Name == commandData.Sender.Username);
            if (p == null)
                p = application.AddPlayer(commandData.SenderIdString, commandData.Sender.Username);

            string challengeName = commandData.Arguments[0]; 
            challengeName = Regex.Replace(challengeName, "[-]", " ");
            string postingPlayerId = p.DiscordId;
            string identifier = Regex.Replace(challengeName, "[^0-9]", "");

            int weaponId = 0;
            if (identifier.Length < 1)
                await commandData.Respond($"You forgot a number in the title {Emotes.ERROR}");
            else if (!int.TryParse(commandData.Arguments[1], out weaponId))
                await commandData.Respond($"Invalid input in selected weapon {Emotes.ERROR}");
            else
            {
                Weapon weapon = (Weapon)weaponId;
                string weaponShort = Regex.Replace(weapon.ToString(), "[^A-Z]", "");
                identifier += weaponShort;
                identifier = MakeUniqueIdentifier(identifier);

                application.AddChallenge(challengeName, weapon, postingPlayerId, identifier);
                await commandData.Respond($"`{challengeName} ({identifier})`EMOTE created");
            }
        }

        private string MakeUniqueIdentifier(string identifier)
        {
            int i = 0;
            string newIdentifier = identifier;
            while (application.Challenges.Exists(x => x.Identifier == newIdentifier))
            {
                newIdentifier = identifier + i;
                i++;
            }
            return newIdentifier;
        }
    }
}
