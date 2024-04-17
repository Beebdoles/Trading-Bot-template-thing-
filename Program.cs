using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;
using System.Threading;
using Newtonsoft.Json;

namespace Bot_Core
{
    public class Bot
    {
        public DiscordClient client {get; private set; }
        public CommandsNextExtension commands {get; private set; }

        public async Task RunAsync()
        {
            var json = string.Empty;

            using(var fs = File.OpenRead("config.json"))
            using(var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            ConfigJson configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
            };

            client = new DiscordClient(config);


            DiscordActivity activity = new DiscordActivity();
            activity.ActivityType = ActivityType.Watching;
            activity.Name = "people suffer";

            client.Ready += OnClientReady;

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] {configJson.Prefix},
                EnableDms = false,
                EnableMentionPrefix = true,
                CaseSensitive = false,
                DmHelp = false,
            };

            commands = client.UseCommandsNext(commandsConfig);

            commands.RegisterCommands<Commands.Commands>();
            commands.RegisterCommands<Commands.AccountData>();
            commands.RegisterCommands<Commands.GraphingGenerator>();

            await client.ConnectAsync(activity);
            await Task.Delay(-1);
        }

        private Task OnClientReady(DiscordClient Sender, ReadyEventArgs e)
        {
            Console.WriteLine("Bot is on");
            return Task.CompletedTask;
        }

    }
}