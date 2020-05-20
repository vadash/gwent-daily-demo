using System;
using System.Collections.Generic;
using gwent_daily_reborn.Model.Cards.Monster;

namespace gwent_daily_reborn.Model.GameMemory
{
    internal class Monster2DeckList : IDeckList
    {
        public Monster2DeckList()
        {
            if (Deck.Count != 25)
                throw new ApplicationException("Wrong deck list. Must contain 25 cards");
        }

        public List<Type> Deck { get; } = new List<Type>
        {
            typeof(OldSpeartip),
            typeof(WeavessIncantation),
            typeof(OldSpeartipAsleep),
            typeof(Katakan),
            typeof(Ozzrel),
            typeof(Whispess),
            typeof(Brewess),
            typeof(Weavess),
            typeof(IceGiant),
            typeof(IceGiant),
            typeof(Cyclops),
            typeof(WildHuntRider),
            typeof(WildHuntRider),
            typeof(Ghoul),
            typeof(Ghoul),
            typeof(AlphaWerewolf),
            typeof(Wyvern),
            typeof(NekkerWarrior),
            typeof(NekkerWarrior),
            typeof(Werewolf),
            typeof(Werewolf),
            typeof(Foglet),
            typeof(Foglet),
            typeof(Plumard),
            typeof(Plumard)
        };
    }
}
