using gwent_daily_reborn.Model.Helpers;
using gwent_daily_reborn.Model.Helpers.Mouse;
using gwent_daily_reborn.Model.Helpers.Tooltip;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Control.BotTasks
{
    internal class GiveGg : IBotTask
    {
        public bool Do()
        {
            var mouse = Services.Container.GetInstance<IMouse>();
            var hardware = Services.Container.GetInstance<IHardwareConstants>();
            var toolTip = Services.Container.GetInstance<ITooltip>();
            toolTip.Show("Sending GG");
            mouse.Move(hardware.EndGameGgBtnLocation);
            Utility.SleepSmall();
            mouse.Click();
            mouse.Move(hardware.EndGameCloseBtnLocation);
            Utility.SleepSmall();
            mouse.Click();
            Utility.SleepHuge();
            return true;
        }
    }
}
