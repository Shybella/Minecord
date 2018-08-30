using Discord.Commands;
using MojangSharp.Endpoints;
using MojangSharp.Responses;
using System;
using System.Threading.Tasks;

namespace Minecord
{
    public class lookup : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;

        [Command("lookup"), Summary("Returns info about the minecraft user")]
        public async Task LOOKUP_Async(string minecraft_username)
        {
            try
            {
                
                //do not search usernames longer than 16 characters and less than 3 characters
                if (minecraft_username.Length > 16 || minecraft_username.Length < 3)
                {
                    await ReplyAsync("Error: Account not found!");
                    return;
                }

                UuidAtTimeResponse info = new UuidAtTime(minecraft_username, DateTime.Now).PerformRequestAsync().Result;

                //make sure this is a valid account
                if (!info.IsSuccess)
                {
                    if (info.Error.ErrorMessage == "NotFound")
                    {
                        await ReplyAsync("Error: Account not found!");
                        return;
                    }
                }

                //If valid lets load the profile
                ProfileResponse profile = new Profile(info.Uuid.Value, true).PerformRequestAsync().Result;

                //just incase
                if(!profile.IsSuccess)
                {
                    await ReplyAsync("Error has occured loading profile! Error: " + profile.Error.ErrorMessage);
                    return;
                }

                //send the lookup
                if (info.IsSuccess && profile.IsSuccess)
                {
                    await ReplyAsync($"```UUID: {info.Uuid.Value}\r"
                        + $"Username: {info.Uuid.PlayerName}\r"
                        + $"Legacy: { info.Uuid.Legacy}\r"
                        + $"Demo: {info.Uuid.Demo}\r"
                        + $"Skin: {profile.Properties.SkinUri}```");
                }
            }
            catch (Exception ex)
            {   
                Console.WriteLine(ex);
            }
        }             
    }
}
