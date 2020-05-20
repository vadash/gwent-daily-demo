using System;
using System.Collections.Generic;
using System.Linq;
using gwent_daily_reborn.Model.Cards;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.GameInfo
{
    internal class Mulligan : IMulligan
    {
        private List<Card> MyCards { get; } = new List<Card>();

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

        public int Count()
        {
            return MyCards.Count;
        }

        public void Update(IEnumerable<Card> listOfCards)
        {
            Reset();
            var hardware = Services.Container.GetInstance<IHardwareConstants>();
            foreach (var card in listOfCards)
            {
                if (card.Confidence < hardware.CardDetectionMinimalConfidenceLevel)
                    continue;
                MyCards.Add(card);
            }
        }

        public void Reset()
        {
            MyCards.Clear();
        }
    }
}
