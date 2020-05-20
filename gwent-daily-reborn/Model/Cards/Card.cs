using System;
using System.Collections.Generic;
using System.Drawing;
using gwent_daily_reborn.Model.Control.BotTasks;
using gwent_daily_reborn.Model.GameMemory;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Cards
{
    public abstract class Card
    {
        public enum Rows
        {
            Melee,
            Ranged,
            Enemy
        }

        protected Card(int x, int y, int width, int height, float confidence, string displayName, int value = 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Confidence = confidence;
            DisplayName = displayName;
            Value = value;
            Area = new Rectangle(x, y, width, height);
        }

        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public float Confidence { get; }
        public string DisplayName { get; }
        public int Value { get; }
        public Rectangle Area { get; }

        /// <summary>
        ///     SelectPlaceRegister -> EndPlay
        ///     Example override SelectPlaceRegister -> ChooseX (your code here) -> EndPlay
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="xyType"></param>
        /// <param name="placePoint"></param>
        /// <returns></returns>
        internal virtual bool Play(out ICollection<IBotTask> tasks, Rows xyType = Rows.Melee, Point placePoint = default)
        {
            SelectPlaceRegister(out tasks, xyType, placePoint);
            EndPlay(tasks);
            return true;
        }

        /// <summary>
        ///     SelectCard -> PlaceCard -> RegisterCard
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="xyType"></param>
        /// <param name="placePoint"></param>
        /// <returns></returns>
        internal bool SelectPlaceRegister(out ICollection<IBotTask> tasks, Rows xyType = Rows.Melee, Point placePoint = default)
        {
            tasks = new List<IBotTask>();
            SelectCard(tasks);
            PlaceCard(xyType, tasks, placePoint);
            RegisterCard();
            return true;
        }

        internal static bool EndPlay(ICollection<IBotTask> tasks)
        {
            var emptyArea = Services.Container.GetInstance<IHardwareConstants>().BoardEmptySpaceLocation;
            tasks.Add(new MouseMoveTask(emptyArea));
            tasks.Add(new LeftMouseClick());
            tasks.Add(new SleepTask(1000));
            return true;
        }

        internal bool RegisterCard()
        {
            Services.Container.GetInstance<IGameMemory>().RegisterCard(GetType());
            return true;
        }

        /// <summary>
        ///     Pick a card from hand
        /// </summary>
        /// <param name="tasks"></param>
        internal bool SelectCard(ICollection<IBotTask> tasks)
        {
            tasks.Add(new TooltipTask("Playing " + DisplayName));
            tasks.Add(new MouseMoveTask(Area));
            tasks.Add(new SleepTask(800));
            tasks.Add(new LeftMouseClick());
            return true;
        }

        private static void PlaceCard(Rows row, ICollection<IBotTask> tasks, Point placePoint = default)
        {
            if (placePoint != default)
                tasks.Add(new MouseMoveTask(placePoint.X, placePoint.Y));
            else
                switch (row)
                {
                    case Rows.Melee:
                        var zone1 = Services.Container.GetInstance<IHardwareConstants>().OurMeleeRowLocation;
                        tasks.Add(new MouseMoveTask(zone1));
                        break;
                    case Rows.Ranged:
                        var zone2 = Services.Container.GetInstance<IHardwareConstants>().OurRangedRowLocation;
                        tasks.Add(new MouseMoveTask(zone2));
                        break;
                    case Rows.Enemy:
                    default:
                        throw new ArgumentOutOfRangeException(nameof(row), row, null);
                }
            tasks.Add(new LeftMouseClick());
            tasks.Add(new SleepTask(800));
        }
    }
}
