using CustomKitchenDeliveries.Models;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    class AddScoreCommand : BaseCommand
    {
        public override bool NeedMod => false;
        public override int ExpectedArguments => 2;

        public override async Task Execute(ClientCommand commandData)
        {
            string senderName = commandData.Sender.Username;
            string challengeIdentifier = commandData.Arguments[0].ToUpper();
            string clearTime = commandData.Arguments[1];
            IReadOnlyCollection<Discord.Attachment> attachments = commandData.DiscordMessage.Attachments;
            Challenge challenge = application.Challenges.Find(x => x.Identifier == challengeIdentifier);

            if (!CommandValidator.ValidClearTimeFormat(clearTime))
                await commandData.Respond($"Clear time format is wrong {Emotes.ERROR}");
            else if (!CommandValidator.HasAttachment(commandData.DiscordMessage))
                await commandData.Respond($"No image was attached {Emotes.ERROR}");
            else if (challenge != null)
                await commandData.Respond($"Challenge does not exist {Emotes.ERROR}");
            else
            {
                Player player = application.Players.Find(x => x.Name == senderName);
                if (player == null)
                    player = application.AddPlayer(commandData.SenderIdString, senderName);

                string imageName = "";
                foreach (var attachment in commandData.DiscordMessage.Attachments)
                {
                    imageName = DownloadImage(attachment.Url, attachment.Filename);
                }

                application.AddScore(player.DiscordId, Score.TimeStringToInt(clearTime), imageName, challenge.Id);
                await commandData.Respond($"Your score was added! {Emotes.SUCCESS}");
            }
        }

        private string DownloadImage(string url, string currentFileName)
        {
            string extension = currentFileName.Split('.').Last();
            string fileName = FilenamePicker(extension);
            WebClient webClient = new WebClient();
            webClient.DownloadFileAsync(new Uri(url), Environment.CurrentDirectory + $"/Files/{fileName}");
            return fileName;
        }

        private string FilenamePicker(string extension)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "/Files/");
            string fileName = "" + (directoryInfo.GetFiles().Length + 1);
            return fileName + $".{extension}";
        }
    }
}
