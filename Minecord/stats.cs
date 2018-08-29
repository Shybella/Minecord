using Discord.Commands;
using MojangSharp.Endpoints;
using MojangSharp.Responses;
using System;
using System.Threading.Tasks;
using static MojangSharp.Endpoints.Statistics;

namespace Minecord
{
    public class stats : ModuleBase<SocketCommandContext>
    {
        [Command("stats")]
        public async Task STATS_Async()
        {
            try
            {
                StatisticsResponse stats = new Statistics(Item.MinecraftAccountsSold).PerformRequestAsync().Result;

                if (stats.IsSuccess)
                {
                    await ReplyAsync($"  Total Minecraft accounts sold: {stats.Total}");
                    await ReplyAsync($"  Last 24h: {stats.Last24h}");
                    await ReplyAsync($"  Average sell/s: {stats.SaleVelocity}");
                }
                else
                {
                    await ReplyAsync("Something went wrong! Error: " + stats.Error.ErrorMessage ?? stats.Error.Exception.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
