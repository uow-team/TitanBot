using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace TitanBot.Modules
{
    [Name("Item")]
    public class ItemModule : ModuleBase<SocketCommandContext>
    {
        [Command("info"), Alias("i")]
        public Task Say([Remainder]string text)
            => ReplyAsync(text);

        [Group("item"), Name("Info")]
        [RequireContext(ContextType.Guild)]
        public class Set : ModuleBase
        {
            [Command("item"), Priority(1)]
            [Summary("Lookup item information")]
            public Task Nick([Remainder]string name)
                => Nick(Context.User as SocketGuildUser, name);

            [Command("item"), Priority(0)]
            public async Task Nick(SocketGuildUser user, [Remainder]string name)
            {
                await user.ModifyAsync(x => x.Nickname = name);
                await ReplyAsync($"{user.Mention} I changed your name to **{name}**");
            }
        }
    }
}
