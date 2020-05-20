using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using gwent_daily_reborn.Model.Cards;
using gwent_daily_reborn.Model.Cards.Extra;
using gwent_daily_reborn.Model.Cards.Leader;
using gwent_daily_reborn.Model.Control.BotTasks;
using gwent_daily_reborn.Model.Helpers;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.GameInfo
{
    internal class Hand : IHand
    {
        private List<Card> MyCards { get; } = new List<Card>();

        public IEnumerable<Card> GetCards()
        {
            return MyCards.AsReadOnly();
        }

        public int Count(Type type)
        {
            return MyCards.Count(card => card.GetType() == type);
        }

        public bool Contain(Type type)
        {
            return Count(type) > 0;
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

        public void Update(IEnumerable<Card> listOfCards, bool bLeader)
        {
            Reset();
            var hardware = Services.Container.GetInstance<IHardwareConstants>();
            foreach (var card in listOfCards)
            {
                if (card.Confidence < hardware.CardDetectionMinimalConfidenceLevel)
                    continue;
                var center = new Point(card.X + card.Width / 2, card.Y + card.Height / 2);
                if (Utility.IsInZone(hardware.HandLocation, center) ||
                    card.GetType() == typeof(TacticalAdvantage))
                    MyCards.Add(card);
            }
            if (bLeader)
            {
                Card leaderCard;
                switch (BotSettings.Leader)
                {
                    case Leaders.WoodlandSpirit:
                        var pos = Utility.PickRandomSpot(hardware.LeaderLocation);
                        leaderCard = new WoodlandSpirit(pos.X, pos.Y, 0, 0, 1f);
                        break;
                    default:
                        throw new ApplicationException(BotSettings.Leader + " not implemented yet");
                }
                MyCards.Add(leaderCard);
            }
        }

        public void Reset()
        {
            MyCards.Clear();
        }

        public bool Play(IEnumerable<Type> cardsList, out ICollection<IBotTask> tasks, Card.Rows row = Card.Rows.Melee, Point playPoint = default)
        {
            tasks = new List<IBotTask>();
            foreach (var type in cardsList)
            {
                var card = GetCard(type);
                return card != null && card.Play(out tasks, row, playPoint);
            }
            return false;
        }

        public bool Play(Type cardType, out ICollection<IBotTask> tasks, Card.Rows row = Card.Rows.Melee, Point playPoint = default)
        {
            var card = GetCard(cardType);
            if (card == null)
            {
                tasks = new List<IBotTask>();
                return false;
            }
            return card.Play(out tasks, row, playPoint);
        }
    }
}
