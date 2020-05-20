using gwent_daily_reborn.Model.Helpers.Mouse;
using gwent_daily_reborn.Model.Helpers.Tooltip;
using gwent_daily_reborn.Model.Settings;
using System;
using System.Threading;

namespace gwent_daily_reborn.Model.Control.BotTasks
{
    internal class BmTask : IBotTask
    {
        private const int BmPercent = 4;

        public bool Do()
        {
            var hardware = Services.Container.GetInstance<IHardwareConstants>();
            var mouse = Services.Container.GetInstance<IMouse>();
            var next = new Random().NextDouble() * 100;
            if (next < BmPercent)
            {
                Services.Container.GetInstance<ITooltip>().Show($"Current BM chance is {BmPercent}%");
                mouse.Move(hardware.EmoteButton);
                mouse.Click();
                Thread.Sleep(500);
                if (next < BmPercent/2)
                {
                    mouse.Move(hardware.Emote3BadMove);
                }
                else
                {
                    mouse.Move(hardware.Emote5HurryUp);
                }
                mouse.Click();
            }
            Thread.Sleep(5000);
            return true;
        }
    }
}
