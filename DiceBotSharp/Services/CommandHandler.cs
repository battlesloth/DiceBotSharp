using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiceBotSharp.Services
{
    public class CommandHandler
    {
        private CommandService commands;
        private readonly DiscordSocketClient client;
        private readonly IConfiguration config;
        private readonly IServiceProvider services;

        public CommandHandler(IServiceProvider services)
        {
            this.services = services;
            config = services.GetRequiredService<IConfiguration>();
            client = services.GetRequiredService<DiscordSocketClient>();
            commands = services.GetRequiredService<CommandService>();

            commands.CommandExecuted += CommandExecutedAsync;
            client.MessageReceived += HandleCommand;
        }


        public async Task InitializeAsync()
        {
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
        }

        public async Task HandleCommand(SocketMessage parameterMessage)
        {
            // Don't handle the command if it is a system message
            var message = parameterMessage as SocketUserMessage;
            if (message == null) return;

            // Don't listen to bots
            if (message.Source != MessageSource.User)
            {
                return;
            }

            // Mark where the prefix ends and the command begins
            int argPos = 0;

            var prefix = char.Parse(config["prefix"]);


            // Determine if the message has a valid prefix, adjust argPos
            if (!(message.HasMentionPrefix(client.CurrentUser, ref argPos) ||
                  message.HasCharPrefix(prefix, ref argPos)))
            {
                return;
            }

            var context = new SocketCommandContext(client, message);

            await commands.ExecuteAsync(context, argPos, services);
            
        }

        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!command.IsSpecified)
            {
                Console.WriteLine($"Command {command.Value.Name} requested by {context.User} does not exist");
                return;
            }

            if (result.IsSuccess)
            {
                Console.WriteLine($"Command {command.Value.Name} executed by {context.User}");
                return;
            }

            await context.Channel.SendMessageAsync($"I'm sorry {context.User.Username}, I can't do that.");
        }

    }
}