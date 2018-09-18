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
            return Regex.Match(clearTime, "[0-4][0-9]'[0-5][0-9]\"[0-9][0-9]").Success;
        }

        public static bool ValidChallengeName(string challengeName)
        {
            return true;
        }

        public static bool HasAttachment(SocketMessage message)
        {
            return message.Attachments.Count > 0;
        }
    }
}
