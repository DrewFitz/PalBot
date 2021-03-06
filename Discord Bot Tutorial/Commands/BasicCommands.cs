using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Emzi0767.Utilities;
using Newtonsoft.Json;

namespace Discord_Bot_Tutorial.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Returns pong")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel
                .SendMessageAsync("Pong")
                .ConfigureAwait(false);
        }

        [Command("add")]
        [Description("Adds two numbers together")]
        public async Task Add(CommandContext ctx,
            [Description("First number")] int numberOne,
            [Description("Second number")] int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync($"{numberOne + numberTwo}")
                .ConfigureAwait(false);
        }

        [Command("dieroll")]
        [Description("Roll a die with any number of sides")]
        public async Task Roll(CommandContext ctx,
            [Description("The number of sides on the die.")]
            int sides)
        {
            var roll = new Random().Next(1, sides + 1);
            await ctx.Channel
                .SendMessageAsync($"{roll}")
                .ConfigureAwait(false);
        }

        [Command("gameidea")]
        [Description("Generate a game idea")]
        public async Task GameIdea(CommandContext ctx)
        {
            // BASED ON THE BAFTA YGD GAME IDEA GENERATOR
            var environments = new string[]
            {
                "Ocean",
                "Cave",
                "Space",
                "Jungle",
                "Inside a Computer",
                "Supermarket",
                "City",
                "Castle",
                "Alternate Reality",
                "At Home"
            };

            var goals = new string[]
            {
                "Escape",
                "Survive",
                "Reach A Destination",
                "Find All Items",
                "Use All Items",
                "Complete The Puzzle",
                "Destroy Objects",
                "Remove All Enemies",
                "Rescue",
                "Capture"
            };

            var rules = new string[]
            {
                "Can't Touch The Floor",
                "Can't Do The Same Thing Twice In A Row",
                "Bounce Off Walls",
                "Can Only Select Connecting Itmes",
                "Can Only Move Forwards",
                "One Life Only",
                "Avoid #Enemies",
                "Must Not #Be Seen",
                "Limited Time",
                "Limited Inventory"
            };

            var genres = new string[]
            {
                "Puzzle",
                "Adventure",
                "Arcade",
                "Educational",
                "Strategy",
                "Casual",
                "Racing",
                "Action",
                "Role Play (RPG)",
                "Simulation"
            };

            var wildcards = new string[]
            {
                "Multiplayer",
                "Fruit",
                "Household Chores",
                "Something Spooky",
                "Colours",
                "Fairy Tale",
                "Music",
                "AR/VR",
                "Magic Spell",
                "Robots",
                "One Touch"
            };

            var r = new Random();

            var environment = environments[r.Next(0, environments.Length)];
            var goal = goals[r.Next(0, goals.Length)];
            var rule = rules[r.Next(0, rules.Length)];
            var genre = genres[r.Next(0, genres.Length)];
            var wildcard = wildcards[r.Next(0, wildcards.Length)];

            var output = $"***Environment:*** {environment}\n" +
                         $"***Goal:*** {goal}\n" +
                         $"***Rule:*** {rule}\n" +
                         $"***Genre:*** {genre}\n" +
                         $"***Wildcard:*** {wildcard}";


            var embed = new DiscordEmbedBuilder
            {
                Title = "Game Idea",
                Description = output,
                Color = DiscordColor.HotPink,
                
            }.Build();

            await ctx.Channel
                .SendMessageAsync(embed: embed)
                .ConfigureAwait(false);
        }
    }
}