using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using gwent_daily_reborn.Model;
using gwent_daily_reborn.Model.Control.Strategy;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn
{
    internal static class Program
    {
        private const string AllowedChars =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#@$^*()";

        private static readonly Random Random = new Random();

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static void Main(string[] args)
        {
            ControlBot.AssembleDi();
            Console.Title = RandomStrings(8, 16);
            GreetUser();
            ControlBot.RunBotThread();
            ControlBot.RunKeyThread();
            while (!BotSettings.TerminateAsap)
                Thread.Sleep(1000);
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static void GreetUser()
        {
            var culture = CultureInfo.CurrentCulture.ToString().ToLower();

            Write("Welcome to gwent daily ");
            var version = Assembly.GetEntryAssembly().GetName().Version;
            Write($"{version.Major}.{version.Minor} β ", ConsoleColor.Yellow);
            WriteLine("version");

            WriteLine($"Loaded <{Services.Container.GetInstance<IStrategy>().Description}>", ConsoleColor.DarkGray);

            if (culture.StartsWith("ru-"))
            {
                Write("Не забудьте переключить язык игры GWENT на  ");
                WriteLine("ENGLISH ", ConsoleColor.Red);

                Write("Нажмите ");
                Write("F9 ", ConsoleColor.Green);
                Write("для старта и");
                Write("F10/Tab ", ConsoleColor.Green);
                WriteLine("для паузы");

                WriteLine("Сначала бота лучше испытать в тренировке", ConsoleColor.DarkGray);
            }
            else
            {
                Write("Use ");
                Write("F9 ", ConsoleColor.Green);
                Write("to start bot and ");
                Write("F10/Tab ", ConsoleColor.Green);
                WriteLine("to pause");

                WriteLine("Please dont leave bot unattended for long time", ConsoleColor.DarkGray);
            }
            if (!culture.StartsWith("en-"))
            {
                Write("Make sure you run GWENT in  ");
                Write("ENGLISH ", ConsoleColor.Red);
                WriteLine("language");
            }
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static void WriteLine(string text, ConsoleColor? ForegroundColor = ConsoleColor.White, ConsoleColor? BackgroundColor = null)
        {
            Write(text + Environment.NewLine, ForegroundColor, BackgroundColor);
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static void Write(string text, ConsoleColor? ForegroundColor = ConsoleColor.White, ConsoleColor? BackgroundColor = null)
        {
            if (BackgroundColor != null && ForegroundColor != null)
            {
                var oldBackground = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Blue;
                var oldForeground = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(text);
                Console.BackgroundColor = oldBackground;
                Console.ForegroundColor = oldForeground;
            }
            else if (BackgroundColor == null && ForegroundColor == null)
            {
                Console.Write(text);
            }
            else if (BackgroundColor == null)
            {
                var oldForeground = Console.ForegroundColor;
                Console.ForegroundColor = ForegroundColor.Value;
                Console.Write(text);
                Console.ForegroundColor = oldForeground;
            }
            else if (ForegroundColor == null)
            {
                var oldBackground = Console.BackgroundColor;
                Console.BackgroundColor = BackgroundColor.Value;
                Console.Write(text);
                Console.BackgroundColor = oldBackground;
            }
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static string RandomStrings(int min, int max)
        {
            return RandomStrings(AllowedChars, min, max, 1, Random)
                .Aggregate("", (current, randomString) => current + randomString);
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static IEnumerable<string> RandomStrings(
            string allowedChars,
            int minLength,
            int maxLength,
            int count,
            Random rng)
        {
            var chars = new char[maxLength];
            var setLength = allowedChars.Length;

            while (count-- > 0)
            {
                var length = rng.Next(minLength, maxLength + 1);

                for (var i = 0; i < length; ++i)
                    chars[i] = allowedChars[rng.Next(setLength)];

                yield return new string(chars, 0, length);
            }
        }
    }
}
