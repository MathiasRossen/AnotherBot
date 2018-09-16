using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CustomKitchenDeliveries.Models;
using Discord.WebSocket;

namespace CustomKitchenDeliveries
{
    public class BotController
    {
        List<string> weaponEmotes = new List<string>()
        {
            ":shield:",
            ":crossed_swords:",
            ":knife:",
            ":dagger:",
            ":hammer:",
            ":saxophone:",
            ":fork_and_knife:",
            ":wrench:",
            ":fencer:",
            ":bomb:",
            ":gun:",
            ":rocket:",
            ":beetle:",
            ":bow_and_arrow:",
        };

        ChallengeDatabase challengeDatabase;

        public BotController()
        {
            challengeDatabase = new ChallengeDatabase("TestDB");
        }

        public async Task HandleMessage(SocketMessage message)
        {
            string content = message.Content;
            if (content[0] == '!')
                await HandleCommand(message);
        }

        private async Task HandleCommand(SocketMessage message)
        {
            ClientCommand command = new ClientCommand(message.Content, message);

            switch (command.Name)
            {
                case "!list":
                    await ListChallenges(command);
                    break;

                case "!show":
                    command.ExpectedArguments = 1;
                    if (command.Valid)
                        await ShowChallenge(command);
                    else
                        await message.Channel.SendMessageAsync($"Expected {command.ExpectedArguments} arguments :frog:");
                    break;

                case "!addscore":
                    command.ExpectedArguments = 2;
                    if (command.Valid)
                        await AddScore(command);
                    else
                        await message.Channel.SendMessageAsync($"Expected {command.ExpectedArguments} arguments :frog:");
                    break;

                case "!addchallenge":
                    command.ExpectedArguments = 2;
                    if (command.Valid)
                        await AddChallenge(command);
                    else
                        await message.Channel.SendMessageAsync($"Expected {command.ExpectedArguments} arguments :frog:");
                    break;

                case "!legend":
                    await ShowLegend(command);
                    break;

                // One-liners
                case "!sns":
                    await command.Channel.SendMessageAsync(weaponEmotes[0]);
                    break;
                case "!db":
                    await command.Channel.SendMessageAsync(weaponEmotes[1]);
                    break;
                case "!ls":
                    await command.Channel.SendMessageAsync(weaponEmotes[2]);
                    break;
                case "!gs":
                    await command.Channel.SendMessageAsync(weaponEmotes[3]);
                    break;
                case "!ha":
                    await command.Channel.SendMessageAsync(weaponEmotes[4]);
                    break;
                case "!hh":
                    await command.Channel.SendMessageAsync(weaponEmotes[5]);
                    break;
                case "!swa":
                    await command.Channel.SendMessageAsync(weaponEmotes[6]);
                    break;
                case "!cb":
                    await command.Channel.SendMessageAsync(weaponEmotes[7]);
                    break;
                case "!la":
                    await command.Channel.SendMessageAsync(weaponEmotes[8]);
                    break;
                case "!gl":
                    await command.Channel.SendMessageAsync(weaponEmotes[9]);
                    break;
                case "!lbg":
                    await command.Channel.SendMessageAsync(weaponEmotes[10]);
                    break;
                case "!hbg":
                    await command.Channel.SendMessageAsync(weaponEmotes[11]);
                    break;
                case "!ig":
                    await command.Channel.SendMessageAsync(weaponEmotes[12]);
                    break;
                case "!bow":
                    await command.Channel.SendMessageAsync(weaponEmotes[13]);
                    break;
            }
        }

        private async Task ListChallenges(ClientCommand command)
        {
            IList<Challenge> challenges = challengeDatabase.Challenges;
            if (challenges.Count >= 1)
            {
                string list = "";
                foreach (Challenge c in challenges)
                {
                    Player creator = challengeDatabase.Players.Find(x => x.Id == c.PlayerId);
                    list += $"```{c.Name} ({c.Identifier})\nWeapon: {c.Weapon.ToString()}, Posted by: {creator.Name}\n\n```";
                }
                await command.Channel.SendMessageAsync(list);
            }
            else
                await command.Channel.SendMessageAsync("There are no challenges :frog:");
        }

        private async Task ShowChallenge(ClientCommand command)
        {
            string identifier = command.Arguments[0].ToUpper();

            Challenge c = challengeDatabase.Challenges.Find(x => x.Identifier == identifier);
            if (c != null)
                await ShowLeaderboard(c, command.Channel);
            else
                await command.Channel.SendMessageAsync($"Challenge with identifier {identifier} does not exist :frog:");
        }

        private async Task AddScore(ClientCommand command)
        {
            string sender = command.Sender;
            string challengeIdentifier = command.Arguments[0].ToUpper();
            string clearTime = command.Arguments[1];

            if (!CommandValidator.ValidClearTimeFormat(clearTime))
            {
                await command.Channel.SendMessageAsync("Clear time format is wrong :frog:");
            }
            else
            {
                Player p = challengeDatabase.Players.Find(x => x.Name == sender);
                if (p == null)
                    p = challengeDatabase.AddPlayer(sender);

                Challenge c = challengeDatabase.Challenges.Find(x => x.Identifier == challengeIdentifier);
                if (c != null)
                {
                    string imageName = "";
                    foreach (var attachment in command.DiscordMessage.Attachments)
                    {
                        imageName = DownloadImage(attachment.Url, attachment.Filename);
                    }
                    challengeDatabase.AddScore(p.Id, Score.TimeStringToInt(clearTime), imageName, c.Id);
                    await command.Channel.SendMessageAsync("Your score was added! :first_place:");
                }
                else
                {
                    await command.Channel.SendMessageAsync("Challenge does not exist :frog:");
                }
            }
        }

        private async Task AddChallenge(ClientCommand command)
        {
            Player p = challengeDatabase.Players.Find(x => x.Name == command.Sender);
            if (p == null)
                p = challengeDatabase.AddPlayer(command.Sender);

            string challengeName = command.Arguments[0];
            challengeName = Regex.Replace(challengeName, "[_]", " ");
            int postingPlayerId = p.Id;
            string identifier = Regex.Replace(challengeName, "[^0-9]", "");

            int weaponId = 0;
            if (identifier.Length < 1)
                await command.Channel.SendMessageAsync("You forgot a number in the title :frog:");
            else if (!int.TryParse(command.Arguments[1], out weaponId))
                await command.Channel.SendMessageAsync("Invalid input in selected weapon :frog:");
            else {
                Weapon weapon = (Weapon)weaponId;
                string weaponShort = Regex.Replace(weapon.ToString(), "[^A-Z]", "");
                identifier += weaponShort;
                identifier = MakeUniqueIdentifier(identifier);

                challengeDatabase.AddChallenge(challengeName, weapon, postingPlayerId, identifier);
                await command.Channel.SendMessageAsync($"`{challengeName} ({identifier})`{weaponEmotes[(int)weapon]} created");
            }
        }

        private async Task ShowLegend(ClientCommand command)
        {
            string legend =
                $"{weaponEmotes[0]} `Sword and Shield`\n" +
                $"{weaponEmotes[1]} `Dual Blades`\n" +
                $"{weaponEmotes[2]} `Long Sword`\n" +
                $"{weaponEmotes[3]} `Great Sword`\n" +
                $"{weaponEmotes[4]} `Hammer`\n" +
                $"{weaponEmotes[5]} `Hunting Horn`\n" +
                $"{weaponEmotes[6]} `Switch Axe`\n" +
                $"{weaponEmotes[7]} `Charge Blade`\n" +
                $"{weaponEmotes[8]} `Lance`\n" +
                $"{weaponEmotes[9]} `Gunlance`\n" +
                $"{weaponEmotes[10]} `Light Bowgun`\n" +
                $"{weaponEmotes[11]} `Heavy Bowgun`\n" +
                $"{weaponEmotes[12]} `Insect Glaive`\n" +
                $"{weaponEmotes[13]} `Bow`";
            await command.Channel.SendMessageAsync(legend);
        }

        private string DownloadImage(string url, string currentFileName)
        {
            string extension = currentFileName.Split('.').Last();
            string fileName = FilenamePicker(extension);
            WebClient webClient = new WebClient();
            webClient.DownloadFileAsync(new Uri(url), Environment.CurrentDirectory + $"\\Files\\{fileName}");
            return fileName;
        }

        private string FilenamePicker(string extension)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "\\Files\\");
            string fileName = "" + (directoryInfo.GetFiles().Length + 1);
            return fileName + $".{extension}";
        }

        private async Task ShowLeaderboard(Challenge challenge, ISocketMessageChannel channel)
        {
            string caption = $"`{challenge.Name}`{weaponEmotes[(int)challenge.Weapon]}";
            await channel.SendMessageAsync(caption);

            List<Score> orderedScores = challengeDatabase.Scores.Where(x => x.ChallengeId == challenge.Id).OrderBy(x => x.ClearTime).Take(3).ToList();
            if (orderedScores.Count == 0)
                await channel.SendMessageAsync("No scores yet! :frog:");

            for (int i = 0; i < orderedScores.Count; i++)
            {
                caption = "";
                Score score = orderedScores[i];
                Player player = challengeDatabase.Players.Find(x => x.Id == score.PlayerId);
                caption += $".\n`#{i + 1}: {player.Name}`";
                await channel.SendFileAsync(score.ImageSource, caption);
            }
        }

        private string MakeUniqueIdentifier(string identifier)
        {
            int i = 0;
            string newIdentifier = identifier;
            while(challengeDatabase.Challenges.Exists(x => x.Identifier == newIdentifier))
            {
                newIdentifier = identifier + i;
                i++;
            }
            return newIdentifier;
        }
    }
}