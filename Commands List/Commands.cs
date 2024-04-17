using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.IO;
using System.Net;

namespace Bot_Core.Commands
{
    public class Commands : BaseCommandModule
    {
        [Command("ping")]
        [Description("check if bot is online")]
        public async Task ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("pong").ConfigureAwait(false);
        }
        
        [Command("money")]
        [Description("WIP, gives money for now")]
        public async Task money(CommandContext ctx, [Description("amount to add")] int money)
        {
            long id = (long)ctx.Member.Id;
            List<Account> list = AccountData.convertToObject(AccountData.readData());
            foreach(Account user in list)
            {
                if(user.userID == id)
                {
                    user._money += money;
                }
            }
            AccountData.writeData(list);
            await ctx.Channel.SendMessageAsync("Added " + money + " money to your account").ConfigureAwait(false);
        }

        [Command("profile")]
        [Description("view a user's profile")]
        public async Task profile(CommandContext ctx, [Description("mentioned user")] string mentionedUser)
        {
            Account tempAccount = new Account();
            long id = (long)ctx.Message.MentionedUsers.First().Id;
            List<Account> list = AccountData.convertToObject(AccountData.readData());
            foreach(Account user in list)
            {
                if(user.userID == id)
                {
                    tempAccount = user;
                }
            }
            DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder();
            embedBuilder.Title = ctx.Message.MentionedUsers.First().Username + "'s profile";
            embedBuilder.AddField("id: ", Convert.ToString(id), false);
            embedBuilder.AddField("money: ", Convert.ToString(tempAccount._money), false);
            embedBuilder.WithThumbnail(ctx.Message.MentionedUsers.First().AvatarUrl, 10, 10);
            DiscordEmbed embed = embedBuilder.Build();
            
            await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
        }

    }
}