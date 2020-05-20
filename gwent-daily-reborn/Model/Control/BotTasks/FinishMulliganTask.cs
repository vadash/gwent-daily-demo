using gwent_daily_reborn.Model.Helpers;
using gwent_daily_reborn.Model.Helpers.Keyboard;
using gwent_daily_reborn.Model.Helpers.Mouse;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Control.BotTasks
{
    internal class FinishMulliganTask : IBotTask
    {
        public bool Do()
        {
            var zone = Services.Container.GetInstance<IHardwareConstants>().FinishMulliganButton;
            Services.Container.GetInstance<IMouse>().Move(zone);
            Utility.SleepSmall();
            Services.Container.GetInstance<IMouse>().Click();
            Utility.SleepMedium();
            Services.Container.GetInstance<IKeyboard>().Press(Messaging.VKeys.Enter);
            Utility.SleepSmall();
            zone = Services.Container.GetInstance<IHardwareConstants>().BoardEmptySpaceLocation;
            Services.Container.GetInstance<IMouse>().Move(zone);
            Utility.SleepHuge();
            return true;
        }
    }
}
