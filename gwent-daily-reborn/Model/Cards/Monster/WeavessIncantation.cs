using System.Collections.Generic;
using System.Drawing;
using gwent_daily_reborn.Model.Control.BotTasks;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Cards.Monster
{
    internal class WeavessIncantation : Card
    {
        public WeavessIncantation(int x, int y, int width, int height, float confidence, int value = 2)
            : base(x, y, width, height, confidence, "WeavessIncantation", value)
        {
        }

        internal override bool Play(out ICollection<IBotTask> tasks, Rows xyType = Rows.Melee, Point placePoint = default)
        {
            return SelectPlaceRegister(out tasks, Rows.Melee, placePoint) && AddDelay(tasks) && EndPlayModified(tasks);
        }

        private static bool AddDelay(ICollection<IBotTask> tasks)
        {
            tasks.Add(new SleepTask(1000));
            return true;
        }

        private static bool EndPlayModified(ICollection<IBotTask> tasks)
        {
            var emptyArea = Services.Container.GetInstance<IHardwareConstants>().MulliganEmptySpaceLocation;
            tasks.Add(new MouseMoveTask(emptyArea));
            tasks.Add(new LeftMouseClick());
            tasks.Add(new SleepTask(1000));
            return true;
        }
    }
}
