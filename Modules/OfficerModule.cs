using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using TitanBotX.Services;

namespace TitanBot.Modules
{
    [Name("Officer")]
    [RequireContext(ContextType.Guild)]
    public class ModeratorModule : ModuleBase<SocketCommandContext>
    {
        private DatabaseService DbService { get; set; }

        public ModeratorModule()
        {
            DbService = DbService == null ? new DatabaseService() : DbService;
        }

        [Command("kick")]
        [Summary("Kick the specified user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task Kick([Remainder]SocketGuildUser user)
        {
            await ReplyAsync($"cya {user.Mention} :wave:");
            await user.KickAsync();
        }

        [Command("AddMember")]
        [Summary("Adds the specified member to the DB")]
        public async Task AddMember([Remainder]SocketGuildUser user)
        {
            var result = DbService.AddMember(user.Nickname, user.Id.ToString(), 0);
            await ReplyAsync($"{result}");
            await user.KickAsync();
        }

        
    }
}
