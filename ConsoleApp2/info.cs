using Discord.Commands;
using MojangSharp.Endpoints;
using MojangSharp.Responses;
using System;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class info : ModuleBase<SocketCommandContext>
    {
        [Command("info")]
        public async Task INFO_Async(string minecraft_username)
        {
            try
            {
                UuidAtTimeResponse info = new UuidAtTime(minecraft_username, DateTime.Now).PerformRequestAsync().Result;
                ProfileResponse profile = new Profile(info.Uuid.Value, true).PerformRequestAsync().Result;
                if (info.IsSuccess)
                {
                    await ReplyAsync($"```UUID: {info.Uuid.Value}\r"
                        + $"Username: {info.Uuid.PlayerName}\r"
                        + $"Legacy: { info.Uuid.Legacy}\r"
                        + $"Demo: {info.Uuid.Demo}\r"
                        + $"Skin: {profile.Properties.SkinUri.ToString()}```\r");               
                }
            }
            catch (Exception ex)
            {
                await ReplyAsync("Invalid username!");     
                Console.WriteLine(ex); //Dump to console and log
            }
        }           
    }
}
