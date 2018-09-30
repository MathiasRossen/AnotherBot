using CustomKitchenDeliveries.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomKitchenDeliveries.Commands
{
    class CatCommand : BaseCommand
    {
        public override bool NeedMod => false;
        public override int ExpectedArguments => 0;

        public override async Task Execute(ClientCommand commandData)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("User-Agent", "FluffyBot");
            string json= httpClient.GetStringAsync("http://aws.random.cat/meow").Result;
            string link = Regex.Replace(json, "({\"file\":\"|\"}|\\\\)", "");
            await commandData.Respond(link);
        }
    }
}
