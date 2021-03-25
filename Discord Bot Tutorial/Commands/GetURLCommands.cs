using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json;

namespace Discord_Bot_Tutorial.Commands
{
    public class GetURLCommands : BaseCommandModule
    {
        #region Utility
        private async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
        #endregion

        [Command("insult")]
        [Description("Generate a sick burn")]
        public async Task Insult(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var insult = await GetAsync("https://evilinsult.com/generate_insult.php?lang=en");

            await ctx.Channel
                .SendMessageAsync(insult)
                .ConfigureAwait(false);
        }

        #region Shiba commands
        public struct ShibaResponse
        {
            [JsonProperty("message")] 
            public string imageURL;
        }

        [Command("shiba")]
        [Description("Post a random shiba pic")]
        public async Task Shiba(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var response = await GetAsync("https://dog.ceo/api/breed/shiba/images/random");
            var shibaResponse = JsonConvert.DeserializeObject<ShibaResponse>(response);
            var imageURL = shibaResponse.imageURL;
            var decodedImageURL = HttpUtility.UrlDecode(imageURL);

            var embed = new DiscordEmbedBuilder
            {
                ImageUrl = decodedImageURL
            }.Build();

            await ctx.Channel
                .SendMessageAsync(embed: embed)
                .ConfigureAwait(false);
        }

        [Command("shiba2")]
        [Description("Post a random shiba pic")]
        public async Task Shiba2(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var response = await GetAsync("http://shibe.online/api/shibes?count=1");
            var shibaResponse = JsonConvert.DeserializeObject<string[]>(response);
            var imageURL = shibaResponse.FirstOrDefault();
            if (imageURL == null)
            {
                await ctx.Channel
                    .SendMessageAsync("Could not load shiba image. :sob:")
                    .ConfigureAwait(false);

                return;
            }

            var decodedImageURL = HttpUtility.UrlDecode(imageURL);

            var embed = new DiscordEmbedBuilder
            {
                ImageUrl = decodedImageURL
            }.Build();

            await ctx.Channel
                .SendMessageAsync(embed: embed)
                .ConfigureAwait(false);
        }
        
        #endregion
    }
}