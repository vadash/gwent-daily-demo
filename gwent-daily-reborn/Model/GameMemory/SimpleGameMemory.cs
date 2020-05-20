using System;
using System.Collections.Generic;
using System.Linq;
using gwent_daily_reborn.Model.Helpers;

namespace gwent_daily_reborn.Model.GameMemory
{
    internal class GameMemory : IGameMemory, IResetAfterGame
    {
        private const int DeckSize = 25;

        public GameMemory()
        {
            ResetAfterGame();
        }

        private List<Type> DeckList { get; set; }
        private List<Type> GraveyardList { get; set; }
        private List<Type> PlayedList { get; set; }

        /// <inheritdoc />
        public bool RegisterCard(Type playedCard)
        {
            foreach (var card in DeckList)
                if (card == playedCard)
                {
                    PlayedList.Add(playedCard);
                    return DeckList.Remove(playedCard);
                }
            return false;
        }

        /// <inheritdoc />
        public void NewRound()
        {
            foreach (var playedCard in PlayedList)
                GraveyardList.Add(playedCard);
            PlayedList = new List<Type>(DeckSize);
        }

        /// <inheritdoc />
        public bool InDeck(Type playedCard)
        {
            foreach (var card in DeckList)
                if (card == playedCard)
                    return true;
            return false;
        }

        /// <inheritdoc />
        public bool InGraveyard(Type playedCard)
        {
            return GraveyardList.Any(card => card == playedCard);
        }

        public IEnumerable<Type> GetPlayed()
        {
            return PlayedList.AsReadOnly();
        }

        public IEnumerable<Type> GetGy()
        {
            return GraveyardList.AsReadOnly();
        }

        public IEnumerable<Type> GetDeck()
        {
            return DeckList.AsReadOnly();
        }

        public bool DeleteFromGraveyard(Type cardType)
        {
            foreach (var card in GraveyardList)
                if (card == cardType)
                    return GraveyardList.Remove(cardType);
            return false;
        }

        public bool AddToGraveyard(Type cardType)
        {
            GraveyardList.Add(cardType);
            return true;
        }

        /// <inheritdoc />
        public void ResetAfterGame()
        {
            DeckList = Services.Container.GetInstance<IDeckList>().Deck;
            GraveyardList = new List<Type>(DeckSize);
            PlayedList = new List<Type>(DeckSize);
        }
    }
}
