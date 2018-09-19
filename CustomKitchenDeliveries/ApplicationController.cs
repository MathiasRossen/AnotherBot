using System;
using System.Collections.Generic;
using System.Text;
using CustomKitchenDeliveries.Models;
using Discord.WebSocket;

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

        public ConfigHelper ConfigHelper { get; private set; }

        public List<ulong> Mods { get; private set; }
        public List<ulong> RespondChannels { get; private set; }

        public List<Player> Players { get; private set; }
        public List<Challenge> Challenges { get; private set; }
        public List<Score> Scores { get; private set; }

        private ApplicationController()
        {
            ConfigHelper = new ConfigHelper();
            challengeDatabase = new ChallengeDatabase(ConfigHelper.Database);
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

        public void AddScore(string playerId, int clearTime, string imageName, int challengeId)
        {
            Score s = Scores.Find(x => x.PlayerDiscordId == playerId);
            if (s != null)
            {
                s.ClearTime = clearTime;
                s.ImageName = imageName;
                challengeDatabase.UpdateScore(s);
            }
            else
            {
                s = new Score() { ClearTime = clearTime, PlayerDiscordId = playerId, ImageName = imageName, ChallengeId = challengeId };
                Scores.Add(s);
                challengeDatabase.AddScore(s);
            }
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

        public bool IsMod(SocketUser user)
        {
            SocketGuildUser guildUser = user as SocketGuildUser;
            return guildUser.GuildPermissions.ManageGuild || IsMod(user.Id);
        }

        public bool IsMod(ulong userId)
        {
            foreach (ulong modUserId in Mods)
            {
                if (userId == modUserId)
                    return true;
            }
            return false;
        }

    }
}
