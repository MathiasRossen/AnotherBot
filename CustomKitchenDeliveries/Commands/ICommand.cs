using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    interface ICommand
    {
        bool NeedMod { get; }
        int ExpectedArguments { get; }
        Task Execute(ClientCommand commandData);
        bool CanExecute(SocketUser user);
    }
}
