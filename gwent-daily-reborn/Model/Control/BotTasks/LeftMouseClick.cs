using gwent_daily_reborn.Model.Helpers.Mouse;

namespace gwent_daily_reborn.Model.Control.BotTasks
{
    internal class LeftMouseClick : IBotTask
    {
        public bool Do()
        {
            Services.Container.GetInstance<IMouse>().Click();
            return true;
        }
    }
}
