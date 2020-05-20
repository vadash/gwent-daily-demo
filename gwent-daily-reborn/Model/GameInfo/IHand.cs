using System;
using System.Collections.Generic;
using System.Drawing;
using gwent_daily_reborn.Model.Cards;
using gwent_daily_reborn.Model.Control.BotTasks;

namespace gwent_daily_reborn.Model.GameInfo
{
    internal interface IHand
    {
        IEnumerable<Card> GetCards();
        int Count(Type type);
        bool Contain(Type type);
        Card GetCard(Type type);
        int Count();
        void Update(IEnumerable<Card> listOfCards, bool bLeader);
        void Reset();
        bool Play(IEnumerable<Type> cardsList, out ICollection<IBotTask> tasks, Card.Rows row = Card.Rows.Melee, Point playPoint = default);
        bool Play(Type cardType, out ICollection<IBotTask> tasks, Card.Rows row = Card.Rows.Melee, Point playPoint = default);
    }
}
