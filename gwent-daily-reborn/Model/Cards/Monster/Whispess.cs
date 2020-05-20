using System;
using System.Collections.Generic;
using System.Drawing;
using gwent_daily_reborn.Model.Control.BotTasks;
using gwent_daily_reborn.Model.Helpers.Keyboard;

namespace gwent_daily_reborn.Model.Cards.Monster
{
    internal class Whispess : Card
    {
        public Whispess(int x, int y, int width, int height, float confidence, int value = 7)
            : base(x, y, width, height, confidence, "Whispess", value)
        {
        }

        internal override bool Play(out ICollection<IBotTask> tasks, Rows xyType = Rows.Melee, Point placePoint = default)
        {
            return SelectPlaceRegister(out tasks, xyType, placePoint) && Choose(tasks) && EndPlay(tasks);
        }

        private static bool Choose(ICollection<IBotTask> tasks)
        {
            var random = new Random();
            if (random.NextDouble() > 0.5)
                tasks.Add(new KeyboardTask(Messaging.VKeys.Up));
            var newRoll = random.NextDouble();
            if (newRoll < 0.25)
                tasks.Add(new KeyboardTask(Messaging.VKeys.Left));
            else if (newRoll < 0.5)
                tasks.Add(new KeyboardTask(Messaging.VKeys.Right));
            tasks.Add(new KeyboardTask(Messaging.VKeys.Up));
            tasks.Add(new KeyboardTask(Messaging.VKeys.Enter));
            return true;
        }
    }
}
