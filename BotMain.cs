using System;
using DSharpPlus;

namespace Bot_Core
{
    class BotMain
    {
        static void Main(String[] args)
        {
            Bot bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}