using System;
using System.Collections.Generic;

namespace gwent_daily_reborn.Model.GameMemory
{
    internal interface IGameMemory
    {
        /// <summary>
        ///     Remove from deck, add to played
        /// </summary>
        /// <param name="playedCard"></param>
        /// <returns></returns>
        bool RegisterCard(Type playedCard);

        /// <summary>
        ///     Populate graveyard
        /// </summary>
        void NewRound();

        /// <summary>
        ///     Check for a card in deck
        /// </summary>
        /// <param name="playedCard"></param>
        /// <returns></returns>
        bool InDeck(Type playedCard);

        /// <summary>
        ///     Check for a card in GY
        /// </summary>
        /// <param name="playedCard"></param>
        /// <returns></returns>
        bool InGraveyard(Type playedCard);

        bool DeleteFromGraveyard(Type cardType);

        bool AddToGraveyard(Type cardType);

        IEnumerable<Type> GetPlayed();
        IEnumerable<Type> GetGy();
        IEnumerable<Type> GetDeck();
    }
}
