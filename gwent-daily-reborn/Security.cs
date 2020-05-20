using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable HeuristicUnreachableCode
#pragma warning disable 162

namespace gwent_daily_reborn
{
    internal static class Security
    {
        [DllImport("kernel32.dll")]
        private static extern uint GetEnvironmentVariable(string lpName, StringBuilder lpBuffer, uint nSize);
        
        [Obfuscation(Feature = "inline", Exclude = false)]
        private static DateTime EndTrialHere { get; } = DateTime.Now.AddMinutes(new Random().Next(65, 70));
        
        /// <summary>
        /// Checks if themida active
        /// </summary>
        /// <returns>true - we are cracked, false - all good</returns>
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        [Obfuscation(Feature = "inline", Exclude = false)]
        public static bool WeCracked1()
        {
#if DEBUG
            return false;
#endif
            try
            {
                var result = new StringBuilder(100);
                // 0 - trial, 1 - lic
                GetEnvironmentVariable("WLRegGetStatus", result, 100);
                return !int.TryParse(result.ToString(), out var regStatus) || regStatus >= 2;
            }
            catch (Exception)
            {
                return true;
            }
        }
        
        /// <summary>
        /// Checks if themida active
        /// </summary>
        /// <returns>true - we are cracked, false - all good</returns>
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        [Obfuscation(Feature = "inline", Exclude = false)]
        public static bool WeCracked2()
        {
#if DEBUG
            return false;
#endif
            try
            {
                var regStatus = new StringBuilder(100);
                // 2.460
                GetEnvironmentVariable("WLGetVersion", regStatus, 100);
                return regStatus.ToString() != "2" + "." + "4" + "6" + "0";
            }
            catch (Exception)
            {
                return true;
            }
        }
        
        /// <summary>
        /// Checks trial
        /// </summary>
        /// <returns>true - expired, we should exit app</returns>
        [Obfuscation(Feature = "inline", Exclude = false)]
        internal static bool TrialExpired()
        {
#if DEBUG
            return false;
#endif
            try
            {
                var result = new StringBuilder(100);
                // 0 - trial, 1 - lic
                GetEnvironmentVariable("WLRegGetStatus", result, 100);
                if (!int.TryParse(result.ToString(), out var regStatus))
                    return true;
                if (regStatus == 1)
                    return false;
                if (EndTrialHere > DateTime.Now)
                    return false;
                return true;
            }
            catch (Exception)
            {
                // ignored
            }
            return true;
        }
    }
}