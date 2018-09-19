using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CustomKitchenDeliveries.Models;
using SQLite;

namespace CustomKitchenDeliveries
{
    class ChallengeDatabase
    {
        SQLiteConnection database;

        public List<Challenge> Challenges
        {
            get { return database.Table<Challenge>().ToList(); }
        }

        public List<Score> Scores
        {
            get { return database.Table<Score>().ToList(); }
        }

        public List<Player> Players
        {
            get { return database.Table<Player>().ToList(); }
        }

        public List<ulong> Mods
        {
            get
            {
                List<ulong> mods = new List<ulong>();
                foreach(Mod m in database.Table<Mod>().ToList())
                {
                    mods.Add(ulong.Parse(m.DiscordId));
                }
                return mods;
            }
        }

        public List<ulong> Channels
        {
            get
            {
                List<ulong> channels = new List<ulong>();
                foreach (Channel c in database.Table<Channel>().ToList())
                {
                    channels.Add(ulong.Parse(c.DiscordId));
                }
                return channels;
            }
        }

        public ChallengeDatabase(string databaseName)
        {
            string databasePath = Path.Combine(Environment.CurrentDirectory, databaseName + ".db");
            database = new SQLiteConnection(databasePath);
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            database.CreateTable<Challenge>();
            database.CreateTable<Score>();
            database.CreateTable<Player>();
            database.CreateTable<Mod>();
            database.CreateTable<Channel>();
        }

        public void AddChallenge(Challenge challenge)
        {
            database.Insert(challenge);
        }

        public void AddScore(Score score)
        {
            database.Insert(score);
        }

        public void UpdateScore(Score score)
        {
            database.Update(score);
        }

        public void AddPlayer(Player player)
        {
            database.Insert(player);
        }

        // Should probably change channel and mod, but they'll stay like this for now
        public void AddChannel(ulong channelId)
        {
            Channel c = new Channel() { DiscordId = channelId.ToString() };
            database.Insert(c);
        }

        public void AddMod(ulong userId)
        {
            Mod m = new Mod() { DiscordId = userId.ToString() };
            database.Insert(m);
        }
    }
}
