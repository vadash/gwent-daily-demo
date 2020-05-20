using System.Collections.Generic;
using System.Drawing;
using gwent_daily_reborn.Model.Control.BotTasks;
using gwent_daily_reborn.Model.Helpers.Keyboard;

namespace gwent_daily_reborn.Model.Cards.Monster
{
    internal class Brewess : Card
    {
        public Brewess(int x, int y, int width, int height, float confidence, int value = 5)
            : base(x, y, width, height, confidence, "Brewess", value)
        {
        }

        internal override bool Play(out ICollection<IBotTask> tasks, Rows xyType = Rows.Melee, Point placePoint = default)
        {
            return SelectPlaceRegister(out tasks, xyType, placePoint) && Choose(tasks) && EndPlay(tasks);
        }

        private static bool Choose(ICollection<IBotTask> tasks)
        {
            tasks.Add(new KeyboardTask(Messaging.VKeys.Down));
            tasks.Add(new KeyboardTask(Messaging.VKeys.Down));
            tasks.Add(new KeyboardTask(Messaging.VKeys.Enter));
            tasks.Add(new KeyboardTask(Messaging.VKeys.Left));
            tasks.Add(new KeyboardTask(Messaging.VKeys.Up));
            tasks.Add(new KeyboardTask(Messaging.VKeys.Enter));
            return true;
        }
    }
}
