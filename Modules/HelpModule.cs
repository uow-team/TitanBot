using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Models;
using TitanBot.Services;
using TitanBotX;

namespace TitanBot.Modules
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;
        private readonly IConfigurationRoot _config;

        public HelpModule(CommandService service, IConfigurationRoot config)
        {
            _service = service;
            _config = config;
        }

        [Command("help")]
        public async Task HelpAsync()
        {
            string prefix = _config["prefix"];
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "These are the commands you can use"
            };

            foreach (var module in _service.Modules)
            {
                string description = null;
                foreach (var cmd in module.Commands)
                {
                    var result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                        description += $"{prefix}{cmd.Aliases.First()}\n";
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = module.Name;
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }

            await ReplyAsync("", false, builder.Build());
        }

        [Command("help")]
        public async Task HelpAsync(string command)
        {
            var result = _service.Search(Context, command);

            if (!result.IsSuccess)
            {
                await ReplyAsync($"Sorry, I couldn't find a command like **{command}**.");
                return;
            }

            string prefix = _config["prefix"];
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = $"Here are some commands like **{command}**"
            };

            foreach (var match in result.Commands)
            {
                var cmd = match.Command;

                builder.AddField(x =>
                {
                    x.Name = string.Join(", ", cmd.Aliases);
                    x.Value = $"Parameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}\n" +
                              $"Summary: {cmd.Summary}";
                    x.IsInline = false;
                });
            }

            await ReplyAsync("", false, builder.Build());
        }

        [Command("qod")]
        public async Task QodAsync()
        {
            string prefix = _config["prefix"];
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "Quote of the Day"
            };

            CallServerService callServerService = new CallServerService();

            var response = callServerService.GetResponse<QuoteResponseModel>(Constants.QuoteOfTheDayUrl);

            builder.AddField(x =>
            {
                x.Name = response.Contents.Quotes.FirstOrDefault().Title;
                x.Value = response.Contents.Quotes.FirstOrDefault().Quote;
                x.IsInline = false;
            });

            await ReplyAsync("", false, builder.Build());
        }

        [Command("qrandom")]
        public async Task QrandomAsync()
        {
            string prefix = _config["prefix"];
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "Random Quote"
            };

            CallServerService callServerService = new CallServerService();

            var response = callServerService.GetResponse<QuoteResponseModel>(Constants.QuoteRandomUrl);

            builder.AddField(x =>
            {
                x.Name = response.Contents.Quotes.FirstOrDefault().Title;
                x.Value = response.Contents.Quotes.FirstOrDefault().Quote;
                x.IsInline = false;
            });

            await ReplyAsync("", false, builder.Build());
        }

        [Command("joke")]
        public async Task JokeAsync()
        {
            string prefix = _config["prefix"];
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "Random Joke"
            };

            CallServerService callServerService = new CallServerService();

            var response = callServerService.GetResponse<JokeModel >(Constants.JokeApiUrl);

            builder.AddField(x =>
            {
                x.Name = $"{response.Category}";
                x.Value = response.Type.Equals("twopart") ? $"{response.Setup}\n\n{response.Delivery}" : response.Joke;
                x.IsInline = false;
            });

            await ReplyAsync("", false, builder.Build());
        }
    }
}
