using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using gwent_daily_reborn.Model.Helpers.Keyboard;

namespace gwent_daily_reborn.Model.Helpers
{
    internal static class Utility
    {
        private static readonly Stopwatch LastRandomDelayTimer = new Stopwatch();

        static Utility()
        {
            LastRandomDelayTimer.Start();
        }

        public static Random Random { get; } = new Random();

        public static void Concede()
        {
            Services.Container.GetInstance<IKeyboard>().Press(Messaging.VKeys.Escape);
            SleepSmall();
            Services.Container.GetInstance<IKeyboard>().Press(Messaging.VKeys.Enter);
            SleepSmall();
        }

        public static int GenerateHash(string str)
        {
            return str.GetHashCode();
        }

        public static int GetSleepTime(int min, int max)
        {
            return Random.Next(min, max);
        }

        public static Point GetRandomPointInArea(Rectangle zone, float indent = 0.3f)
        {
            var x1 = (int) (zone.X + zone.Width * indent);
            var y1 = (int) (zone.Y + zone.Height * indent);
            var x2 = (int) (zone.X + zone.Width * (1 - indent));
            var y2 = (int) (zone.Y + zone.Height * (1 - indent));
            return new Point(Random.Next(x1, x2), Random.Next(y1, y2));
        }

        public static bool Sleep(int max)
        {
            Sleep(max, (int) 1.2 * max);
            return true;
        }

        private static bool Sleep(int min, int max)
        {
            Thread.Sleep(Random.Next(min, max));
            return true;
        }

        /// <summary>
        ///     5000ms sleep
        /// </summary>
        public static bool SleepHuge()
        {
            Sleep(5000);
            return true;
        }

        /// <summary>
        ///     3000ms sleep
        /// </summary>
        public static bool SleepBig()
        {
            Sleep(3000);
            return true;
        }

        /// <summary>
        ///     800ms sleep
        /// </summary>
        public static bool SleepMedium()
        {
            Sleep(800);
            return true;
        }

        /// <summary>
        ///     500ms sleep
        /// </summary>
        public static bool SleepSmall()
        {
            Sleep(500);
            return true;
        }

        /// <summary>
        ///     Returns random point in rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        public static Point PickRandomSpot(Rectangle rectangle, float indent = 0.3f)
        {
            var x = Random.Next(
                (int) (rectangle.Left + rectangle.Width * indent),
                (int) (rectangle.Right - rectangle.Width * indent));
            var y = Random.Next(
                (int) (rectangle.Top + rectangle.Height * indent),
                (int) (rectangle.Bottom - rectangle.Height * indent));
            return new Point(x, y);
        }

        public static bool IsInZone(Rectangle rectangle, Point point)
        {
            return point.X >= rectangle.Left &&
                   point.X <= rectangle.Right &&
                   point.Y <= rectangle.Bottom &&
                   point.Y >= rectangle.Top;
        }

        /// <summary>
        ///     Special sleep 25 25 50 75 100 150 ms
        /// </summary>
        /// <param name="i"></param>
        public static void SmartSleep(int i)
        {
            switch (i)
            {
                default:
                    Sleep(25, 50);
                    break;
                case 3:
                    Sleep(50);
                    break;
                case 2:
                    Sleep(75);
                    break;
                case 1:
                    Sleep(100);
                    break;
                case 0:
                    Sleep(150);
                    break;
            }
        }
    }
}
