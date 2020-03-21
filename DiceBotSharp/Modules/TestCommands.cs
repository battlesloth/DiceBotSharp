using System.Text;
using System.Threading.Tasks;
using DiceBotSharp.Utility;
using Discord.Commands;

namespace DiceBotSharp.Modules
{
    public class TestCommands : ModuleBase
    {
        [Command("hello")]
        public async Task HelloCommand()
        {
            var sb = new StringBuilder();

            var user = UtilityMethods.GetUsername(Context);

            sb.AppendLine($"Hello, {user}.");
            sb.AppendLine("Would you like to play a game?");
            sb.AppendLine("Maybe Pandemic?");
            sb.AppendLine("No, I think you are already playing that one.");
            sb.AppendLine("...");
            sb.AppendLine("Perhaps Nuclear Annihilation?");

            await ReplyAsync(sb.ToString());
        }

        [Command("help")]
        public async Task HelpCommand()
        {
            var sb = new StringBuilder();

            sb.AppendLine("*** DiceBot Help ***");
            sb.AppendLine("Start a message with ! to invoke the A.I.");
            sb.AppendLine("It\'s not case sensitive.");
            sb.AppendLine();
            sb.AppendLine("Roll with advantage: !d20+");
            sb.AppendLine("Roll with disadvantage: !d20-");
            sb.AppendLine("Roll a single die: !d4");
            sb.AppendLine("Roll multiple dice: !6d8");
            sb.AppendLine("You can use positive modifiers: !4d6+2");
            sb.AppendLine("And negative modifiers: !3d4-2");

            await ReplyAsync(sb.ToString());
        }
    }
}
