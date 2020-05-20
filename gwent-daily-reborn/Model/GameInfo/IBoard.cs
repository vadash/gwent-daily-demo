using System;
using System.Collections.Generic;
using System.Drawing;
using gwent_daily_reborn.Model.Cards;

namespace gwent_daily_reborn.Model.GameInfo
{
    internal interface IBoard
    {
        int MeleeSize { get; }
        int RangedSize { get; }
        bool Contain(Type type);
        int Count(Type type);
        Card GetCard(Type type);
        IEnumerable<Card> GetCards();
        int Count();
        void Update(IEnumerable<Card> listOfCards);
        void Reset();
        Rectangle RandomTarget();
    }
}
