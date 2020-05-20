using System;
using System.Collections.Generic;
using gwent_daily_reborn.Model.Cards;

namespace gwent_daily_reborn.Model.GameInfo
{
    internal interface IMulligan
    {
        bool Contain(Type type);
        int Count(Type type);
        Card GetCard(Type type);
        int Count();
        void Update(IEnumerable<Card> listOfCards);
        void Reset();
    }
}
