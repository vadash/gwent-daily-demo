using System;
using System.Collections.Generic;

namespace gwent_daily_reborn.Model.GameMemory
{
    internal interface IDeckList
    {
        List<Type> Deck { get; }
    }
}
