namespace gwent_daily_reborn.Model.Settings
{
    public enum Factions
    {
        Mo
    }

    public enum Leaders
    {
        WoodlandSpirit
    }

    internal static class BotSettings
    {
        public static bool PauseBotAfterGame { get; set; }
        public static bool NeedEmotes { get; set; }
        public static bool QueueRanked { get; set; }
        public static bool NeedPlanRestarts { get; set; }
        public static int PlanRestartTimer { get; set; }
        public static int ShutdownTimer { get; set; }
        public static bool Bot247 { get; set; }
        public static Factions Faction { get; set; }
        public static Leaders Leader { get; set; }
        public static string MainNName { get; set; } = "gwent_monster2";
        public static bool IsRunning { get; set; } = true;
        public static bool TerminateAsap { get; set; }
    }
}
