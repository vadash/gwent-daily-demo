using gwent_daily_reborn.Model.Control.BotTasks;
using System.Collections.Generic;
using System.Drawing;

namespace gwent_daily_reborn.Model.Cards.Monster
{
    internal class Foglet : Card
    {
        public Foglet(int x, int y, int width, int height, float confidence, int value = 3)
            : base(x, y, width, height, confidence, "Foglet", value)
        {
        }

        internal override bool Play(out ICollection<IBotTask> tasks, Rows xyType = Rows.Melee, Point placePoint = default)
        {
            // force play melee row so its easy to target him
            return base.Play(out tasks, Rows.Melee, placePoint);
        }
    }
}
