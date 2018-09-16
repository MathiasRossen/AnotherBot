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
        SQLiteConnection connection;

        public List<Challenge> Challenges
        {
            get { return connection.Table<Challenge>().ToList(); }
        }

        public List<Score> Scores
        {
            get { return connection.Table<Score>().ToList(); }
        }

        public List<Player> Players
        {
            get { return connection.Table<Player>().ToList(); }
        }

        public ChallengeDatabase(string databaseName)
        {
            string databasePath = Path.Combine(Environment.CurrentDirectory, databaseName + ".db");
            connection = new SQLiteConnection(databasePath);
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            connection.CreateTable<Challenge>();
            connection.CreateTable<Score>();
            connection.CreateTable<Player>();
        }

        public Challenge AddChallenge(string name, Weapon weapon, int postingPlayerId, string identifier)
        {
            Challenge c = new Challenge() { Name = name, Weapon = weapon, PlayerId = postingPlayerId, Identifier = identifier };
            connection.Insert(c);
            return Challenges.Find(x => x.Identifier == identifier);
        }

        public void AddScore(int playerId, int clearTime, string imageName, int challengeId)
        {
            Score s = new Score() { ClearTime = clearTime, PlayerId = playerId, ImageName = imageName, ChallengeId = challengeId };
            connection.Insert(s);
        }

        public Player AddPlayer(string username)
        {
            Player p = new Player() { Name = username };
            connection.Insert(p);
            return Players.Find(x => x.Name == username);
        }
    }
}
