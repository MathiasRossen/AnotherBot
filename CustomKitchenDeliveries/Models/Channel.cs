using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CustomKitchenDeliveries.Models
{
    class Channel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string DiscordId { get; set; } // The SQLite library doesn't use uint64, so i'm saving Discord IDs in a string
        //public string Name { get; set; }
    }
}
