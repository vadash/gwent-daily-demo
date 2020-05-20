using gwent_daily_reborn.Model.Helpers;
using gwent_daily_reborn.Model.Helpers.Keyboard;
using gwent_daily_reborn.Model.Helpers.Mouse;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Control.BotTasks
{
    internal class EndTurn : IBotTask
    {
        public bool Do()
        {
            Services.Container.GetInstance<IKeyboard>().Press(Messaging.VKeys.Space);
            var hardware = Services.Container.GetInstance<IHardwareConstants>();
            Services.Container.GetInstance<IMouse>().Move(hardware.BoardEmptySpaceLocation);
            Utility.SleepBig();
            return true;
        }
    }
}
