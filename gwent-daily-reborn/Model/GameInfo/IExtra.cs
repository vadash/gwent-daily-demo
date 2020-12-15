namespace gwent_daily_reborn.Model.GameInfo
{
    internal interface IExtra
    {
        bool? IsMulligan { get; }
        bool? IsOurTurn { get; }
        bool? IsLeaderOn { get; }
        bool? IsLeaderOff { get; }
        bool? IsEnemyPassed { get; }
        bool? IsEndTurn { get; }
        int OurScore { get; }
        int EnemyScore { get; }
        Rounds CurrentRound { get; }
        bool? IsPickCard { get; }
        bool? NeedStartGame { get; }
        bool? IsEndGame { get; }
        bool? IsModalDialog1Opened { get; }
        bool? IsModalDialog2Opened { get; }
        bool? IsModalDialog3Opened { get; }
        bool? IsModalDialog4Opened { get; }
        bool? MustPlayCard { get; }
        bool? UndoAction { get; }
        bool? EnemyTurn { get; }
        bool? IsModalDialog5Opened { get; set; }
        bool? IsModalDialog6Opened { get; set; }
        bool? IsModalDialog7Opened { get; set; }
        bool? IsModalDialog11Opened { get; }
        bool? IsModalDialog10Opened { get; }
        bool? IsModalDialog9Opened { get; }
        bool? IsModalDialog8Opened { get; }

        void Update(bool isMulligan, bool isOurTurn, bool isLeaderOn, bool isLeaderOff,
            bool isEnemyPassed, bool isEndTurn, int ourScore,
            int enemyScore, Rounds currentRound, bool isPickCard,
            bool needStartGame, bool isEndGame, bool isModalDialog1Opened,
            bool isModalDialog2Opened, bool isModalDialog3Opened, bool isModalDialog4Opened,
            bool isModalDialog5Opened, bool isModalDialog6Opened, bool isModalDialog7Opened,
            bool isModalDialog8Opened, bool isModalDialog9Opened, bool isModalDialog10Opened,
            bool isModalDialog11Opened,
            bool mustPlayCard, bool undoAction, bool enemyTurn);
    }

    public enum Rounds
    {
        /// <summary>
        ///     0-0
        /// </summary>
        FirstRound,

        /// <summary>
        ///     0-1
        /// </summary>
        EnemyLead,

        /// <summary>
        ///     1-0
        /// </summary>
        WeLead,

        /// <summary>
        ///     1-1
        /// </summary>
        Draw
    }
}
