using System.Drawing;
using gwent_daily_reborn.Model.Helpers;
using gwent_daily_reborn.Model.Helpers.Mouse;

namespace gwent_daily_reborn.Model.Control.BotTasks
{
    internal class MouseMoveTask : IBotTask
    {
        public MouseMoveTask(Rectangle area)
        {
            var p = Utility.PickRandomSpot(area);
            X = p.X;
            Y = p.Y;
        }

        public MouseMoveTask(int x, int y)
        {
            X = x;
            Y = y;
        }

        private int X { get; }
        private int Y { get; }

        public bool Do()
        {
            Services.Container.GetInstance<IMouse>().Move(X, Y);
            return true;
        }
    }
}
