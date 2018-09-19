using CustomKitchenDeliveries.Models;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    class ShowChallengeCommand : BaseCommand
    {
        public override bool NeedMod => false;
        public override int ExpectedArguments => 1;

        public override async Task Execute(ClientCommand commandData)
        {
            string identifier = commandData.Arguments[0].ToUpper();
            Challenge c = application.Challenges.Find(x => x.Identifier == identifier);
            if (c != null)
                await ShowLeaderboard(c, commandData.Channel);
            else
                await commandData.Channel.SendMessageAsync($"Challenge with identifier {identifier} does not exist {Emotes.ERROR}");
        }

        private async Task ShowLeaderboard(Challenge challenge, ISocketMessageChannel channel)
        {
            string caption = $"`{challenge.Name}`{Emotes.WeaponsArray[(int)challenge.Weapon]}";
            await channel.SendMessageAsync(caption);

            List<Score> orderedScores = application.Scores.Where(x => x.ChallengeId == challenge.Id).OrderBy(x => x.ClearTime).Take(3).ToList();
            if (orderedScores.Count == 0)
                await channel.SendMessageAsync($"No scores yet! {Emotes.ERROR}");

            for (int i = 0; i < orderedScores.Count; i++)
            {
                caption = "";
                Score score = orderedScores[i];
                Player player = application.Players.Find(x => x.DiscordId == score.PlayerDiscordId);
                caption += $".\n`#{i + 1}: {player.Name}`";
                await channel.SendFileAsync(score.ImageSource, caption);
            }
        }
    }
}
