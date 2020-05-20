using System.Collections.Generic;
using System.Drawing;
using gwent_daily_reborn.Model.Cards.Monster;
using gwent_daily_reborn.Model.Control.BotTasks;
using gwent_daily_reborn.Model.GameInfo;

namespace gwent_daily_reborn.Model.Cards.Extra
{
    internal class TacticalAdvantage : Card
    {
        public TacticalAdvantage(int x, int y, int width, int height, float confidence, int value = 5)
            : base(x, y, width, height, confidence, "TacticalAdvantage", value)
        {
        }

        /// <summary>
        ///     Extra cards requires specific logic
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="xyType"></param>
        /// <param name="placePoint"></param>
        internal override bool Play(out ICollection<IBotTask> tasks, Rows xyType = Rows.Melee, Point placePoint = default)
        {
            var board = Services.Container.GetInstance<IBoard>();
            var targetLocation = board.Contain(typeof(Foglet)) ? board.GetCard(typeof(Foglet)).Area : board.RandomTarget();
            tasks = new List<IBotTask>
            {
                new MouseMoveTask(Area),
                new LeftMouseClick(),
                new MouseMoveTask(targetLocation),
                new LeftMouseClick(),
                new SleepTask(1000)
            };
            EndPlay(tasks);
            return true;
        }
    }
}
