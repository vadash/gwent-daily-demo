using System;
using System.Threading;

namespace gwent_daily_reborn.Model.Control.BotTasks
{
    internal class SleepTask : IBotTask
    {
        public SleepTask(int time = 250)
        {
            Time = time;
        }

        private int Time { get; }

        public bool Do()
        {
            var time = new Random().Next(Time, (int) (Time * 1.2));
            Thread.Sleep(time);
            return true;
        }
    }
}
