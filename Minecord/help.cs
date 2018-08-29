using Discord.Commands;
using MojangSharp.Api;
using MojangSharp.Endpoints;
using MojangSharp.Responses;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Minecord
{
    public class help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HELP_Async()
        {
            try
            {
                await ReplyAsync($"```"
                    + $"!lookup <username> - returns Minecraft account information. \r"
                    + $"!status - returns Mojang service status.\r"
                    + $"!help - returns info related to help topic.\r"
                    + $"!stats - returns info about Minecraft sales statistics.\r"
                    + $"!blacklist - returns total blocked and cracked servers.```");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
