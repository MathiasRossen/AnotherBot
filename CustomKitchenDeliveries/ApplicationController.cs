using System;
using System.Collections.Generic;
using CustomKitchenDeliveries.Models;

namespace CustomKitchenDeliveries
{
    class ApplicationController
    {
        #region Singleton
        private static object padlock = new object();
        private static ApplicationController instance;

        public static ApplicationController Instance
        {
            get
            {
                if (instance == null)
                    lock (padlock)
                    {
                        if (instance == null)
                            instance = new ApplicationController();
                    }

                return instance;
            }
        }
        #endregion

        ChallengeDatabase challengeDatabase;

        public BotConfig BotConfig { get; private set; }
        public string ApplicationPath => BotConfig.ApplicationPath;
        public string ImagePath => BotConfig.ImagePath;

        public List<ulong> Mods { get; private set; }
        public List<ulong> RespondChannels { get; private set; }

        public List<Player> Players { get; private set; }
        public List<Challenge> Challenges { get; private set; }
        public List<Score> Scores { get; private set; }

        private ApplicationController()
        {
            BotConfig = new BotConfig();
            challengeDatabase = new ChallengeDatabase(BotConfig.Database);
            SyncWithDatabase();

            // Adding myself
            AddMod(212240081577181184);
        }

        public Challenge AddChallenge(string name, Weapon weapon, string postingPlayerDiscordId, string identifier)
        {
            Challenge c = new Challenge() { Name = name, Weapon = weapon, PlayerDiscordId = postingPlayerDiscordId, Identifier = identifier };
            Challenges.Add(c);
            challengeDatabase.AddChallenge(c);
            return c;
        }

        public void RemoveChallenge(string identifier)
        {
            Challenge c = Challenges.Find(x => x.Identifier == identifier);
            challengeDatabase.RemoveChallenge(c);
        }

        public void AddScore(string playerId, string clearTimeString, int challengeId)
        {
            Score s = Scores.Find(x => x.PlayerDiscordId == playerId && x.ChallengeId == challengeId);
            if (s != null)
            {
                s.ClearTime = Score.ParseClearTime(clearTimeString);
                s.ClearTimeString = clearTimeString;
                //s.ImageName = imageName;
                challengeDatabase.UpdateScore(s);
            }
            else
            {
                s = new Score() { ClearTime = Score.ParseClearTime(clearTimeString), ClearTimeString = clearTimeString, PlayerDiscordId = playerId, ChallengeId = challengeId };
                Scores.Add(s);
                challengeDatabase.AddScore(s);
            }
        }

        public void RemoveScore(Score score)
        {           
            challengeDatabase.RemoveScore(score);
        }

        public Player AddPlayer(string userId, string username)
        {
            Player p = new Player() { DiscordId = userId, Name = username };
            Players.Add(p);
            challengeDatabase.AddPlayer(p);
            return p;
        }

        public void AddMod(ulong userId)
        {
            Mods.Add(userId);
            challengeDatabase.AddMod(userId);
        }

        public void AddChannel(ulong channelId)
        {
            RespondChannels.Add(channelId);
            challengeDatabase.AddChannel(channelId);
        }

        public void SyncWithDatabase()
        {
            Mods = challengeDatabase.Mods;
            RespondChannels = challengeDatabase.Channels;
            Players = challengeDatabase.Players;
            Challenges = challengeDatabase.Challenges;
            Scores = challengeDatabase.Scores;
        }

    }
}
