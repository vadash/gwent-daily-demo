using gwent_daily_reborn.Model.Helpers;
using gwent_daily_reborn.Model.Helpers.Mouse;
using gwent_daily_reborn.Model.Helpers.Tooltip;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Control.BotTasks
{
    internal class PassTask : IBotTask
    {
        public PassTask(string text)
        {
            Text = text;
        }

        private string Text { get; }

        public bool Do()
        {
            var hardware = Services.Container.GetInstance<IHardwareConstants>();
            Services.Container.GetInstance<ITooltip>().Show(Text);
            Services.Container.GetInstance<IMouse>().Move(hardware.OurTurnDetection.Rectangle);
            Services.Container.GetInstance<IMouse>().Click(3000);
            Services.Container.GetInstance<IMouse>().Move(hardware.BoardEmptySpaceLocation);
            Utility.SleepHuge();
            return true;
        }
    }
}
