using Discord;
using Discord.Commands;

namespace DiceBotSharp.Utility
{
    public static class UtilityMethods
    {
        public static string GetUsername(ICommandContext context)
        {
            var username = context.User.Username;

            try
            {
                var author = context.Message.Author as IGuildUser;
                if (!string.IsNullOrEmpty(author.Nickname))
                {
                    username = author.Nickname;
                }
            }
            catch
            {
                //yum yum
            }

            return username;
        }
    }
}
