using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CustomKitchenDeliveries
{
    public static class CommandValidator
    {
        public static bool ValidClearTimeFormat(string clearTime)
        {
            Match match = Regex.Match(clearTime, "[0-4][0-9]'[0-5][0-9]\"[0-9][0-9]");
            return match.Success;
        }

        public static bool ValidChallengeName(string challengeName)
        {
            return true;
        }
    }
}
