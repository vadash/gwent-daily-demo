using System.Collections.Generic;
using gwent_daily_reborn.Model.Control.BotTasks;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Control.ActionManager
{
    internal class ActionManager : IActionManager
    {
        public void Do(ICollection<IBotTask> tasks)
        {
            if (tasks == null) return;
            foreach (var task in tasks)
                if (BotSettings.IsRunning)
                    task.Do();
                else
                    return;
        }
    }
}
