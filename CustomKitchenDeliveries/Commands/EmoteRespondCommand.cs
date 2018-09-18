using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    class EmoteRespondCommand : BaseCommand
    {
        public override bool NeedMod => false;
        public override int ExpectedArguments => 0;

        public override async Task Execute(ClientCommand commandData)
        {
            string response = "";
            switch (commandData.Name)
            {
                case "!sns":
                    response = Emotes.SNS;
                    break;
                case "!db":
                    response = Emotes.DB;
                    break;
                case "!ls":
                    response = Emotes.LS;
                    break;
                case "!gs":
                    response = Emotes.GS;
                    break;
                case "!ha":
                case "!hammer":
                    response = Emotes.HA;
                    break;
                case "!hh":
                    response = Emotes.HH;
                    break;
                case "!sa":
                    response = Emotes.SA;
                    break;
                case "!cb":
                    response = Emotes.CB;
                    break;
                case "!la":
                case "!lance":
                    response = Emotes.LA;
                    break;
                case "!gl":
                    response = Emotes.GL;
                    break;
                case "!lbg":
                    response = Emotes.LBG;
                    break;
                case "!hbg":
                    response = Emotes.HBG;
                    break;
                case "!ig":
                    response = Emotes.IG;
                    break;
                case "!bow":
                    response = Emotes.BOW;
                    break;
            }
            await commandData.Respond(response);
        }
    }
}
