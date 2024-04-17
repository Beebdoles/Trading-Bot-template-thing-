using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Text;
using System.Collections.Generic;
using System.Text.Json;

namespace Bot_Core.Commands
{
    public class AccountData : BaseCommandModule
    {

        [Command("register")]
        [Description("Registers you on the bot")]
        public async Task register(CommandContext ctx)
        {       
            long id = ((long)ctx.Member.Id);

            List<Account1> accountListRaw = readData();
            List<Account> accountList = convertToObject(accountListRaw);

            int control = 0;
            foreach(Account account in accountList)
            {
                if(account.userID == id)
                {
                    ++control;
                }
            }

            if(control == 0)
            {
                Account newAccount = new Account();
                newAccount.userID = id;
                newAccount._money = 0;
                accountList.Add(newAccount);
                writeData(accountList);
                await ctx.Channel.SendMessageAsync("Successfully registered!").ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync("You have already registered").ConfigureAwait(false);
            }

        }

        public static List<Account1> readData()
        {
            string jsonString = File.ReadAllText(@"C:\Users\Beebd\Documents\VS_Code\C# projects\Trading Bot V2\Users\UserData.json");
            List<Account1> list = JsonSerializer.Deserialize<List<Account1>>(jsonString)!;
            return list;
        }

        public static void writeData(List<Account> list)
        {
            var options = new JsonSerializerOptions {WriteIndented = true};
            string jsonString = JsonSerializer.Serialize(list, options);
            File.WriteAllText(@"C:\Users\Beebd\Documents\VS_Code\C# projects\Trading Bot V2\Users\UserData.json", jsonString);
        }

        public static List<Account> convertToObject(List<Account1> list)
        {
            List<Account> convertedList = new List<Account>();
            for(int i = 0; i < list.Count; ++i)
            {
                Account temp = new Account();
                temp.userID = list[i].userID;
                temp._money = list[i]._money;

                convertedList.Add(temp);
            }
            return convertedList;
        }

    }

    public class Account1
    {
        public long userID {get; set; }
        public int _money {get; set; }
    }

}