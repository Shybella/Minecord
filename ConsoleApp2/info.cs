using Discord.Commands;
using MojangSharp.Endpoints;
using MojangSharp.Responses;
using System;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class info : ModuleBase<SocketCommandContext>
    {
        [Command("lookup")]
        public async Task LOOKUP_Async(string minecraft_username)
        {
            try
            {               
                UuidAtTimeResponse info = new UuidAtTime(minecraft_username, DateTime.Now).PerformRequestAsync().Result;

                //make sure this is a valid account
                if (!info.IsSuccess)
                {
                    if (info.Error.ErrorMessage == "NotFound")
                    {
                        await ReplyAsync("Account not found");
                        return;
                    }
                }

                //If valid lets load the profile
                ProfileResponse profile = new Profile(info.Uuid.Value, true).PerformRequestAsync().Result;

                //just incase
                if(!profile.IsSuccess)
                {
                    await ReplyAsync("Error has occured loading profile! Error: " + profile.Error.ErrorMessage);
                }

                //send the lookup
                if (info.IsSuccess && profile.IsSuccess)
                {
                    await ReplyAsync($"```UUID: {info.Uuid.Value}\r"
                        + $"Username: {info.Uuid.PlayerName}\r"
                        + $"Legacy: { info.Uuid.Legacy}\r"
                        + $"Demo: {info.Uuid.Demo}\r"
                        + $"Skin: {profile.Properties.SkinUri.ToString()}```");
                }
            }
            catch (Exception ex)
            {   
                Console.WriteLine(ex);
            }
        }             
    }
}
