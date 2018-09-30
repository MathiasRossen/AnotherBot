using System.Collections.Generic;
using System.Threading.Tasks;
using CustomKitchenDeliveries.Commands;
using Discord.WebSocket;

namespace CustomKitchenDeliveries
{
    public class BotMain
    {
        // All commands listed here
        Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>
        {
            { "!addscore", new AddScoreCommand() },
            { "!addchallenge", new AddChallengeCommand() },
            { "!list", new ListChallengesCommand() },
            { "!show", new ShowChallengeCommand() },
            { "!legend", new LegendCommand() },
            { "!help", new HelpCommand() },
            { "!removechallenge", new RemoveChallengeCommand() },
            { "!removescore", new RemoveScoreCommand() },
            { "!amimod", new AmIModCommand() },
            { "!cat", new CatCommand() }
        };

        public BotMain()
        {
            // Using this method to have a single reference for one-liners
            EmoteRespondCommand emoteRespondCommand = new EmoteRespondCommand();
            commands.Add("!sns", emoteRespondCommand);
            commands.Add("!db", emoteRespondCommand);
            commands.Add("!gs", emoteRespondCommand);
            commands.Add("!ls", emoteRespondCommand);
            commands.Add("!ha", emoteRespondCommand);
            commands.Add("!hh", emoteRespondCommand);
            commands.Add("!sa", emoteRespondCommand);
            commands.Add("!cb", emoteRespondCommand);
            commands.Add("!la", emoteRespondCommand);
            commands.Add("!gl", emoteRespondCommand);
            commands.Add("!lbg", emoteRespondCommand);
            commands.Add("!hbg", emoteRespondCommand);
            commands.Add("!ig", emoteRespondCommand);
            commands.Add("!bow", emoteRespondCommand);
        }

        public async Task HandleMessage(SocketMessage message)
        {
            string content = message.Content;
            if (content[0] == '!')
                await HandleCommand(message);
        }

        private async Task HandleCommand(SocketMessage message)
        {
            ClientCommand clientCommand = new ClientCommand(message.Content, message);
            if(commands.TryGetValue(clientCommand.Name, out ICommand command))
            {
                if (command.ExpectedArguments <= clientCommand.Count && command.CanExecute(clientCommand.Sender))
                    await command.Execute(clientCommand);
                else
                    await clientCommand.Respond($"Expected {command.ExpectedArguments} arguments {Emotes.ERROR}");
            }
        }
    }
}