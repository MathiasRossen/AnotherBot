﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomKitchenDeliveries.Models
{
    class Player
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string DiscordId { get; set; } // The SQLite library doesn't use uint64, so i'm saving Discord IDs in a string
        public string Name { get; set; }
    }
}
