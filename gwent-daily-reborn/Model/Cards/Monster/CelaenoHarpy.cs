using gwent_daily_reborn.Model.Control.BotTasks;
using gwent_daily_reborn.Model.Helpers.Keyboard;
using System.Collections.Generic;
using System.Drawing;

namespace gwent_daily_reborn.Model.Cards.Monster
{
    internal class CelaenoHarpy : Card
    {
        public CelaenoHarpy(int x, int y, int width, int height, float confidence, int value = 5)
            : base(x, y, width, height, confidence, "CelaenoHarpy", value)
        {
        }

        internal override bool Play(out ICollection<IBotTask> tasks, Rows xyType = Rows.Melee, Point placePoint = default)
        {
            return SelectPlaceRegister(out tasks, xyType, placePoint) && Choose(tasks) && EndPlay(tasks);
        }

        private static bool Choose(ICollection<IBotTask> tasks)
        {
            tasks.Add(new KeyboardTask(Messaging.VKeys.Left));
            tasks.Add(new KeyboardTask(Messaging.VKeys.Enter));
            return true;
        }
    }
}
