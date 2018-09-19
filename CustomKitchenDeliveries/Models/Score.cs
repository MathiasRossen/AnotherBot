using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomKitchenDeliveries.Models
{
    class Score
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ClearTime { get; set; }
        public string ImageName { get; set; }
        [Indexed]
        public int ChallengeId { get; set; }
        [Indexed]
        public string PlayerDiscordId { get; set; } // The SQLite library doesn't use uint64, so i'm saving Discord IDs in a string
        [Ignore]
        public string ImageSource
        {
            get { return Environment.CurrentDirectory + "/Files/" + ImageName; }
        }

        public static int TimeStringToInt(string timeString)
        {
            char[] seperators = { '\'', '"' };
            string[] timeStringSplit = timeString.Split(seperators);
            int minutes = int.Parse(timeStringSplit[0]);
            int seconds = int.Parse(timeStringSplit[1]);
            int milliSeconds = int.Parse(timeStringSplit[2]);
            int total = (minutes * 60 + seconds) * 1000 + milliSeconds * 10;
            return total;
        }
    }
}