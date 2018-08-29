using Discord.Commands;
using MojangSharp.Endpoints;
using MojangSharp.Responses;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Minecord
{
    public class blacklist : ModuleBase<SocketCommandContext>
    {
        [Command("blacklist")]
        public async Task BLOCKED_Async()
        {
            try
            {
                BlockedServersResponse servers = new BlockedServers().PerformRequestAsync().Result;

                if (servers.IsSuccess)
                {                
                    await ReplyAsync($"  {servers.BlockedServers.Count} blocked servers");
                    await ReplyAsync($"  {servers.BlockedServers.FindAll(x => x.Cracked).Count} cracked");
                }
                else
                {
                    await ReplyAsync(servers.Error.ErrorMessage ?? servers.Error.Exception.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
