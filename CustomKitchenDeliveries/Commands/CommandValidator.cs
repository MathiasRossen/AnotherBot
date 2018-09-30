using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CustomKitchenDeliveries.Commands
{
    public static class CommandValidator
    {
        public static bool ValidClearTimeFormat(string clearTime)
        {
            return Regex.Match(clearTime, "^[0-4]?\\d'[0-5]\\d(\"|')\\d\\d$").Success;
        }

        public static bool ValidChallengeName(string challengeName)
        {
            return true;
        }

        public static bool HasAttachment(SocketMessage message)
        {
            return message.Attachments.Count > 0;
        }

        public static bool IsMod(SocketUser user)
        {
            SocketGuildUser guildUser = user as SocketGuildUser;
            if (guildUser == null)
                return IsMod(user.Id);
            return guildUser.GuildPermissions.ManageGuild || IsMod(user.Id);
        }

        public static bool IsMod(ulong userId)
        {
            foreach (ulong modUserId in ApplicationController.Instance.Mods)
            {
                if (userId == modUserId)
                    return true;
            }
            return false;
        }
    }
}
