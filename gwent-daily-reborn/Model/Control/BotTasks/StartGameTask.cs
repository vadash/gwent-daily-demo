using System.Threading;
using gwent_daily_reborn.Model.Helpers;
using gwent_daily_reborn.Model.Helpers.Mouse;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Control.BotTasks
{
    internal class StartGameTask : IBotTask
    {
        public bool Do()
        {
            var mouse = Services.Container.GetInstance<IMouse>();
            var hardware = Services.Container.GetInstance<IHardwareConstants>();
            mouse.Move(hardware.StartGameClickLocation);
            Utility.SleepSmall();
            mouse.Click();
            Utility.SleepHuge();
            return true;
        }
    }
}
