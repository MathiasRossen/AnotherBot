using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomKitchenDeliveries.Models
{
    class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
