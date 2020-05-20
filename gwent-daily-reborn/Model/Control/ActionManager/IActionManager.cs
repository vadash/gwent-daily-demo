using System.Collections.Generic;
using gwent_daily_reborn.Model.Control.BotTasks;

namespace gwent_daily_reborn.Model.Control.ActionManager
{
    internal interface IActionManager
    {
        void Do(ICollection<IBotTask> tasks);
    }
}
