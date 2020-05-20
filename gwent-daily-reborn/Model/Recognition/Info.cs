// ReSharper disable InconsistentNaming

namespace gwent_daily_reborn.Model.Recognition
{
    public enum Info
    {
        /// <summary>
        ///     our turn (we can play card, leader or pass)
        /// </summary>
        ReadyToPlay,

        /// <summary>
        ///     our turn (we have to play card)
        /// </summary>
        MustPlayCard,

        /// <summary>
        ///     enemy turn
        /// </summary>
        DoNothing,

        /// <summary>
        ///     need to bring window to foreground
        /// </summary>
        ActivateGwentWindow,

        /// <summary>
        ///     played card from hand, can play leader or order card
        /// </summary>
        EndTurn,

        /// <summary>
        ///     can mulligan card
        /// </summary>
        Mulligan,

        /// <summary>
        ///     end game, can gg
        /// </summary>
        GiveGG,

        /// <summary>
        ///     screen with selecting card (ex leader ability or ghoul)
        /// </summary>
        PickCard,

        /// <summary>
        ///     need to click start casual/ranked game
        /// </summary>
        StartNewGame,

        /// <summary>
        ///     Ex disconnect, enemy conceded. Should be closable with click
        /// </summary>
        CloseModalDialog1,
        CloseModalDialog2,
        CloseModalDialog3,
        CloseModalDialog4,

        /// <summary>
        /// Stuck in game
        /// </summary>
        Stuck,

        /// <summary>
        /// Enemy turn
        /// </summary>
        EnemyTurn
    }
}
