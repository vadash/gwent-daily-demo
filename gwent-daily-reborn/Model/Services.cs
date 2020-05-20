using System;
using System.IO;
using SimpleInjector;

namespace gwent_daily_reborn.Model
{
    internal static class Services
    {
        private static readonly string SettingsPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GwentDaily");

        static Services()
        {
        }

        public static Container Container { get; set; }
    }
}
