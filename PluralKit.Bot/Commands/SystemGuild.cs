using System.Threading.Tasks;

using DSharpPlus.Entities;
using DSharpPlus.Exceptions;

using PluralKit.Core;

namespace PluralKit.Bot
{
    public class SystemGuild
    {
        private readonly IDatabase _db;
        private readonly ModelRepository _repo;

        public SystemGuild(IDatabase db, ModelRepository repo){
            _db = db;
            _repo = repo;
        }

        public async Task setGuildPrivacy(Context ctx)
        {
            ctx.CheckSystem().CheckGuildContext();

            var level = false;

            if(ctx.Match("private")){
                level = true;
            }
            else if(ctx.Match("public")){
                level = false;
            }
            else{
                
            }
            var patch = new SystemGuildPatch {PrivateGuild = level};
            await _db.Execute(conn => _repo.UpsertSystemGuild(conn, ctx.System.Id, ctx.Guild.Id, patch));

        }

    private async Task<DiscordEmbed> CreateAutoproxyStatusEmbed(Context ctx)
        {
            var eb = new DiscordEmbedBuilder().WithTitle($"Current privacy status (for {ctx.Guild.Name.EscapeMarkdown()})");
            
            var fronters = ctx.MessageContext.LastSwitchMembers;
            var relevantMember = ctx.MessageContext.AutoproxyMode switch
            {
                AutoproxyMode.Front => fronters.Length > 0 ? await _db.Execute(c => _repo.GetMember(c, fronters[0])) : null,
                AutoproxyMode.Member => await _db.Execute(c => _repo.GetMember(c, ctx.MessageContext.AutoproxyMember.Value)),
                _ => null
            };

            

            return eb.Build();
        }

    }
}