using System.Collections.Generic;
using System.Drawing;
using gwent_daily_reborn.Model.Control.BotTasks;

namespace gwent_daily_reborn.Model.Cards.Monster
{
    internal class Nekker : Card
    {
        public Nekker(int x, int y, int width, int height, float confidence, int value = 4)
            : base(x, y, width, height, confidence, "Nekker", value)
        {
        }

        internal override bool Play(out ICollection<IBotTask> tasks, Rows xyType = Rows.Melee, Point placePoint = default)
        {
            // we need to keep melee as free as possible
            return base.Play(out tasks, Rows.Ranged, placePoint);
        }
    }
}
