using gwent_daily_reborn.Model.Helpers;

namespace gwent_daily_reborn.Model.GameInfo
{
    internal class Extra : IExtra, IResetAfterGame
    {
        public bool? IsMulligan { get; private set; }
        public bool? IsOurTurn { get; private set; }
        public bool? IsLeaderOn { get; private set; }
        public bool? IsLeaderOff { get; private set; }
        public bool? IsEnemyPassed { get; private set; }
        public bool? IsEndTurn { get; private set; }
        public int OurScore { get; private set; }
        public int EnemyScore { get; private set; }
        public Rounds CurrentRound { get; private set; }
        public bool? IsPickCard { get; private set; }
        public bool? NeedStartGame { get; private set; }
        public bool? IsEndGame { get; private set; }
        public bool? IsModalDialog1Opened { get; private set; }
        public bool? IsModalDialog2Opened { get; private set; }
        public bool? IsModalDialog3Opened { get; private set; }
        public bool? IsModalDialog4Opened { get; private set; }
        public bool? IsModalDialog7Opened { get; set; }
        public bool? IsModalDialog6Opened { get; set; }
        public bool? IsModalDialog5Opened { get; set; }
        public bool? IsModalDialog11Opened { get; private set; }
        public bool? IsModalDialog10Opened { get; private set; }
        public bool? IsModalDialog9Opened { get; private set; }
        public bool? IsModalDialog8Opened { get; private set; }
        public bool? MustPlayCard { get; private set; }
        public bool? UndoAction { get; private set; }
        public bool? EnemyTurn { get; private set; }

        public void Update(
            bool isMulligan, bool isOurTurn, bool isLeaderOn, bool isLeaderOff,
            bool isEnemyPassed, bool isEndTurn, int ourScore,
            int enemyScore, Rounds currentRound, bool isPickCard,
            bool needStartGame, bool isEndGame, bool isModalDialog1Opened,
            bool isModalDialog2Opened, bool isModalDialog3Opened, bool isModalDialog4Opened,
            bool isModalDialog5Opened, bool isModalDialog6Opened, bool isModalDialog7Opened,
            bool isModalDialog8Opened, bool isModalDialog9Opened, bool isModalDialog10Opened,
            bool isModalDialog11Opened,
            bool mustPlayCard, bool undoAction, bool enemyTurn)
        {
            IsMulligan = isMulligan;
            IsOurTurn = isOurTurn;
            IsLeaderOn = isLeaderOn;
            IsLeaderOff = isLeaderOff;
            IsEnemyPassed = isEnemyPassed;
            IsEndTurn = isEndTurn;
            OurScore = ourScore;
            EnemyScore = enemyScore;

            if (IsMulligan == false && IsOurTurn == true)
                CurrentRound = currentRound;

            IsPickCard = isPickCard;
            NeedStartGame = needStartGame;
            IsEndGame = isEndGame;
            IsModalDialog1Opened = isModalDialog1Opened;
            IsModalDialog2Opened = isModalDialog2Opened;
            IsModalDialog3Opened = isModalDialog3Opened;
            IsModalDialog4Opened = isModalDialog4Opened;
            IsModalDialog5Opened = isModalDialog5Opened;
            IsModalDialog6Opened = isModalDialog6Opened;
            IsModalDialog7Opened = isModalDialog7Opened;
            IsModalDialog8Opened = isModalDialog8Opened;
            IsModalDialog9Opened = isModalDialog9Opened;
            IsModalDialog10Opened = isModalDialog10Opened;
            IsModalDialog11Opened = isModalDialog11Opened;
            MustPlayCard = mustPlayCard;
            UndoAction = undoAction;
            EnemyTurn = enemyTurn;
        }

        public void ResetAfterGame()
        {
            CurrentRound = Rounds.FirstRound;
        }
    }
}
