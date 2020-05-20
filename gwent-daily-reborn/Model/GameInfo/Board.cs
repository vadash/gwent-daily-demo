using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using gwent_daily_reborn.Model.Cards;
using gwent_daily_reborn.Model.Cards.Extra;
using gwent_daily_reborn.Model.Cards.Monster;
using gwent_daily_reborn.Model.Helpers;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.GameInfo
{
    internal class Board : IBoard
    {
        private List<Card> MyCards { get; } = new List<Card>();

        public int MeleeSize { get; private set; }
        public int RangedSize { get; private set; }

        public bool Contain(Type type)
        {
            foreach (var card in MyCards)
                if (card.GetType() == type)
                    return true;
            return false;
        }

        public int Count(Type type)
        {
            return MyCards.Count(card => card.GetType() == type);
        }

        public Card GetCard(Type type)
        {
            foreach (var card in MyCards)
                if (card.GetType() == type)
                    return card;
            return null;
        }

        public IEnumerable<Card> GetCards()
        {
            return MyCards.AsReadOnly();
        }

        public void Update(IEnumerable<Card> listOfCards)
        {
            Reset();
            var hardware = Services.Container.GetInstance<IHardwareConstants>();
            var meleeY = hardware.OurMeleeRowLocation.Y + hardware.OurMeleeRowLocation.Height / 2;
            var rangedY = hardware.OurRangedRowLocation.Y + hardware.OurRangedRowLocation.Height / 2;
            foreach (var card in listOfCards)
            {
                if (card.Confidence < hardware.CardDetectionMinimalConfidenceLevel)
                    continue;
                var center = new Point(card.X + card.Width / 2, card.Y + card.Height / 2);
                if (Utility.IsInZone(hardware.BoardLocation, center))
                {
                    MyCards.Add(card);
                    var cardY = card.Y + card.Height / 2;
                    if (Math.Abs(cardY - meleeY) < Math.Abs(cardY - rangedY))
                        MeleeSize++;
                    else
                        RangedSize++;
                }
            }
        }

        public void Reset()
        {
            MyCards.Clear();
        }

        public Rectangle RandomTarget()
        {
            var tmpList = new List<Rectangle>();
            foreach (var card in MyCards)
                // TODO implement immunity status
                if (card.GetType() != typeof(AlphaWerewolf) &&
                    card.GetType() != typeof(Werewolf) &&
                    card.GetType() != typeof(TacticalAdvantage))
                    tmpList.Add(card.Area);
            return tmpList[new Random().Next(tmpList.Count)];
        }

        public int Count()
        {
            return MyCards.Count;
        }
    }
}
