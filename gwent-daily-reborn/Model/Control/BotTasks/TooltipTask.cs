using gwent_daily_reborn.Model.Helpers.Tooltip;

namespace gwent_daily_reborn.Model.Control.BotTasks
{
    internal class TooltipTask : IBotTask
    {
        public TooltipTask(string text, int time = 5000)
        {
            Text = text;
            Time = time;
        }

        private string Text { get; }
        private int Time { get; }

        public bool Do()
        {
            Services.Container.GetInstance<ITooltip>().Show(Text, Time);
            return true;
        }
    }
}
