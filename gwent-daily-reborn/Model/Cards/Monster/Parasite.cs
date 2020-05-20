using gwent_daily_reborn.Model.Control.BotTasks;
using gwent_daily_reborn.Model.GameInfo;
using System.Collections.Generic;
using System.Drawing;

namespace gwent_daily_reborn.Model.Cards.Monster
{
    internal class Parasite : Card
    {
        public Parasite(int x, int y, int width, int height, float confidence, int value = 6)
            : base(x, y, width, height, confidence, "Parasite", value)
        {
        }

        internal override bool Play(out ICollection<IBotTask> tasks, Rows xyType = Rows.Melee, Point placePoint = default)
        {
            tasks = new List<IBotTask>();
            return SelectCard(tasks) && Choose(tasks) && EndPlay(tasks);
        }

        private bool Choose(ICollection<IBotTask> tasks)
        {
            var board = Services.Container.GetInstance<IBoard>();
            var targetLocation = board.RandomTarget();
            tasks.Add(new MouseMoveTask(Area));
            tasks.Add(new LeftMouseClick());
            tasks.Add(new MouseMoveTask(targetLocation));
            tasks.Add(new LeftMouseClick());
            tasks.Add(new SleepTask(1000));
            return true;
        }
    }
}
