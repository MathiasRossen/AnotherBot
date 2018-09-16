using Discord.WebSocket;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Models
{
    public enum Weapon{ SwordAndShield, DualBlades, LongSword, GreatSword, Hammer, HuntingHorn, SwitchAxe, ChargeBlade, Lance, GunLance, LightBowgun, HeavyBowgun, InsectGlaive, Bow }

    class Challenge
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public Weapon Weapon { get; set; }
        [Indexed]
        public int PlayerId { get; set; }
    }
}
