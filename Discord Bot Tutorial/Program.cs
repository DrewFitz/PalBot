using System;
using System.Diagnostics;

namespace Discord_Bot_Tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}