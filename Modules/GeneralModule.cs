using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using TitanBotX.Services;

namespace TitanBot.Modules
{
    [Name("General")]
    public class GeneralModule : ModuleBase<SocketCommandContext>
    {
        private DatabaseService DbService { get; set; }

        public GeneralModule()
        {
            DbService = DbService == null ? new DatabaseService() : DbService;
        }

        [Command("say"), Alias("s")]
        [Summary("Make the bot say something")]
        public Task Say([Remainder]string text)
            => ReplyAsync(text);

        [Group("set"), Name("Test")]
        [RequireContext(ContextType.Guild)]
        public class Set : ModuleBase
        {
            [Command("nick"), Priority(1)]
            [Summary("Change your nickname to the specified text")]
            [RequireUserPermission(GuildPermission.ChangeNickname)]
            public Task Nick([Remainder]string name)
                => Nick(Context.User as SocketGuildUser, name);

            [Command("nick"), Priority(0)]
            [Summary("Change another user's nickname to the specified text")]
            [RequireUserPermission(GuildPermission.ManageNicknames)]
            public async Task Nick(SocketGuildUser user, [Remainder]string name)
            {
                await user.ModifyAsync(x => x.Nickname = name);
                await ReplyAsync($"{user.Mention} I changed your name to **{name}**");
            }
        }

        [Group("rule"), Name("Rule")]
        [RequireContext(ContextType.Guild)]
        public class Rule : ModuleBase
        {
            private DatabaseService DbService { get; set; }

            public Rule()
            {
                DbService = DbService == null ? new DatabaseService() : DbService;
            }

            [Command("add")]
            [Summary("Adds the specified rule to the DB")]
            public async Task AddRule([Remainder]string text)
            {
                var result = DbService.AddRule(text);
                await ReplyAsync($"{result}");
            }

            [Command("edit")]
            [Summary("Edits specified rule")]
            public async Task Edit(string number, [Remainder]string text)
            {
                if(!int.TryParse(number, out int n)){
                    await ReplyAsync("invalid argument");
                }

                DbService.EditRule(int.Parse(number), text);

                await ReplyAsync($"Editing rule {number}");
            }
        }

        [Command("rules")]
        [Summary("Gets all the rules")]
        public async Task Rules()
        {
            var rules = DbService.GetAllRules();

            var resultString = "";
            foreach (var rule in rules)
            {
                resultString += $"{rule.Number}. {rule.Text}\n";
            }

            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "RULES"
            };

            builder.AddField(x =>
            {
                x.Name = $"{rules.Count} rules";
                x.Value = resultString;
                x.IsInline = false;
            });

            await ReplyAsync("", false, builder.Build());
        }
    }
}
