using Discord.Commands;
using MojangSharp.Endpoints;
using MojangSharp.Responses;
using System;
using System.Threading.Tasks;


namespace ConsoleApp2
{
    public class status : ModuleBase<SocketCommandContext>
    {
        [Command("status")]
        public async Task STATUS_Async()
        {
            //Honestly probably not smart doing this but whatever dude
            try
            {
                ApiStatusResponse status = await new ApiStatus().PerformRequestAsync();
                if (status.IsSuccess)
                {
                    await ReplyAsync($"```Minecraft.net: {status.Minecraft}\r" 
                        + $"Mojang Accounts: {status.MojangAccounts}\r" 
                        + $"Mojang API: {status.MojangApi}\r"
                        + $"Mojang Auth Servers: {status.MojangAutenticationServers}\r"
                        + $"Mojang Sessions: {status.MojangSessionsServer}```");
                }
                else
                {
                    await ReplyAsync(status.Error.Exception == null ? status.Error.ErrorMessage : status.Error.Exception.Message);
                }                 
            }

            catch (Exception ex)
            {
                await ReplyAsync("Mojang API service is offline! Try again later.");
                Console.WriteLine(ex); // Dump to console and log
            }
        }
    }
}
