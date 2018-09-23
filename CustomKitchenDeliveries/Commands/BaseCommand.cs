using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    abstract class BaseCommand : ICommand
    {
        protected ApplicationController application = ApplicationController.Instance;

        public abstract bool NeedMod { get; }
        public abstract int ExpectedArguments { get; }

        public abstract Task Execute(ClientCommand commandData);

        public bool CanExecute(SocketUser user)
        {
            if(NeedMod)
            {
                if (CommandValidator.IsMod(user))
                    return true;
                else
                    return false;
            }
            return true;
        }
    }
}
