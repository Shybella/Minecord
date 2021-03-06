﻿using Discord.Commands;
using MojangSharp.Endpoints;
using MojangSharp.Responses;
using System;
using System.Threading.Tasks;


namespace Minecord
{
    public class status : ModuleBase<SocketCommandContext>
    {
        [Command("status")]
        public async Task STATUS_Async()
        {
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

                    await ReplyAsync("Something went wrong! Error: " + status.Error.Exception == null ? status.Error.ErrorMessage : status.Error.Exception.Message);
                }                 
            }

            catch (Exception ex)
            {
                await ReplyAsync("Mojang API service is offline! Try again later.");
                Console.WriteLine(ex);
            }
        }
    }
}
