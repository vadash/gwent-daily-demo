using System.Collections.Generic;
using System.Drawing;
using Alturos.Yolo.Model;

namespace gwent_daily_reborn.Model.Cards
{
    internal interface ICardFactory
    {
        List<Card> Create(IEnumerable<YoloItem> items, Point basePoint);
    }
}
