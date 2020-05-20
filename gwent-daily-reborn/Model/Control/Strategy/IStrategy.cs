using System.Collections.Generic;
using gwent_daily_reborn.Model.Control.BotTasks;
using gwent_daily_reborn.Model.Recognition;

namespace gwent_daily_reborn.Model.Control.Strategy
{
    internal interface IStrategy
    {
        /// <summary>
        /// Plays card (populates tasks based on info). Just preparing actions
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="info"></param>
        bool Play(out ICollection<IBotTask> tasks, Info info);
        string Description { get; }
    }
}
