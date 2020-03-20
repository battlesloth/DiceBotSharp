using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiceBotSharp.Modules
{
    public class TestCommands : ModuleBase
    {
        [Command("hello")]
        public async Task HelloCommand()
        {
            var sb = new StringBuilder();

            var user = Context.User;

            sb.AppendLine($"Hello, {user.Username}.");
            sb.AppendLine($"Would you like to play a game?");
            sb.AppendLine("Maybe Pandemic?");
            sb.AppendLine("No, I think you are already playing that one.");
            sb.AppendLine("...");
            sb.AppendLine("Perhaps Nuclear Annihilation?");

            await ReplyAsync(sb.ToString());
        }
    }
}
