#region copyright

// Copyright (c) 2020 Project Agonyl

#endregion copyright

using System;
using System.Linq;
using System.Security.Principal;

namespace Agonyl.Shared.Util
{
    public static class CliUtil
    {
        private const string TitlePrefix = "Project Agonyl : ";

        private static readonly string[] Logo = new string[]
        {
            @"    ____               _           __     ___                           __",
            @"   / __ \_________    (_)__  _____/ /_   /   | ____ _____  ____  __  __/ /",
            @"  / /_/ / ___/ __ \  / / _ \/ ___/ __/  / /| |/ __ `/ __ \/ __ \/ / / / / ",
            @" / ____/ /  / /_/ / / /  __/ /__/ /_   / ___ / /_/ / /_/ / / / / /_/ / /  ",
            @"/_/   /_/   \____/_/ /\___/\___/\__/  /_/  |_\__, /\____/_/ /_/\__, /_/   ",
            @"                /___/                       /____/            /____/      ",
        };

        private static readonly string[] Credits = new string[]
        {
            @"by Project Agonyl Team",
        };

        /// <summary>
        /// Writes logo and credits to Console.
        /// </summary>
        /// <param name="consoleTitle"></param>
        /// <param name="color">Color of the logo.</param>
        public static void WriteHeader(string consoleTitle, ConsoleColor color)
        {
            Console.Title = TitlePrefix + consoleTitle;

            Console.ForegroundColor = color;
            WriteLinesCentered(Logo);

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            WriteLinesCentered(Credits);

            Console.ResetColor();
            WriteSeperator();
        }

        /// <summary>
        /// Writes separator in form of 80 underscores to Console.
        /// </summary>
        public static void WriteSeperator()
        {
            Console.WriteLine(string.Empty.PadLeft(Console.WindowWidth, '_'));
        }

        /// <summary>
        /// Writes lines to Console, centering them as a group.
        /// </summary>
        /// <param name="lines"></param>
        private static void WriteLinesCentered(string[] lines)
        {
            var longestLine = lines.Max(a => a.Length);
            foreach (var line in lines)
            {
                WriteLineCentered(line, longestLine);
            }
        }

        /// <summary>
        /// Writes line to Console, centering it either with the string's
        /// length or the given length as reference.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="referenceLength">Set to greater than 0, to use it as reference length, to align a text group.</param>
        private static void WriteLineCentered(string line, int referenceLength = -1)
        {
            if (referenceLength < 0)
            {
                referenceLength = line.Length;
            }

            Console.WriteLine(line.PadLeft(line.Length + Console.WindowWidth / 2 - referenceLength / 2));
        }

        public static void LoadingTitle()
        {
            if (!Console.Title.StartsWith("* "))
            {
                Console.Title = "* " + Console.Title;
            }
        }

        public static void RunningTitle()
        {
            Console.Title = Console.Title.TrimStart('*', ' ');
        }

        /// <summary>
        /// Waits for the return key, and closes the application afterwards.
        /// </summary>
        /// <param name="exitCode"></param>
        /// <param name="wait"></param>
        public static void Exit(int exitCode, bool wait = true)
        {
            if (wait)
            {
                Log.Info("Press Enter to exit.");
                Console.ReadLine();
            }

            Log.Info("Exiting...");
            Environment.Exit(exitCode);
        }

        /// <summary>
        /// Returns whether the application runs with admin rights or not.
        /// </summary>
        public static bool CheckAdmin()
        {
            var id = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(id);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
