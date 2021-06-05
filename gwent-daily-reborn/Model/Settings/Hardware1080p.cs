using System.Drawing;

namespace gwent_daily_reborn.Model.Settings
{
    internal class Hardware1080P : IHardwareConstants
    {
        public int Width => 1920;
        public int Height => 1080;
        public float CardDetectionMinimalConfidenceLevel => 0.65f;

        //public Color LeadScoreColor => Color.FromArgb(216, 192, 120);

        /// <summary>
        ///     Location with mouse icon. Pressing right button will cancel action
        /// </summary>
        public RectoColor UndoAction => new RectoColor(
            new Rectangle(1716, 997, 4, 7),
            Color.FromArgb(12, 242, 169), 10, 32);

        public RectoColor OurHalfCrown => new RectoColor(
            new Rectangle(1837, 613, 22, 31),
            Color.FromArgb(144, 112, 48), 10, 32);

        public RectoColor EnemyHalfCrown => new RectoColor(
            new Rectangle(1838, 421, 22, 31),
            Color.FromArgb(144, 112, 48), 10, 32);

        public RectoString OurTurnDetection => new RectoString(
            new Rectangle(1820, 528, 60, 24),
            "PASS");

        public RectoString EndTurnDetection => new RectoString(
            new Rectangle(1826, 514, 48, 24),
            "END");

        public RectoString MulliganDetection => new RectoString(
            new Rectangle(778, 1010, 59, 19),
            "FINISH");

        public RectoString PickCardDetection => new RectoString(
            new Rectangle(830, 33, 93, 45),
            "PICK");

        public RectoString StartGameDetection => new RectoString(
            new Rectangle(619, 336, 228, 46),
            "STANDARD");

        public Rectangle StartGameClickLocation => new Rectangle(432, 365, 123, 365);

        public RectoString EnemyPassedDetection => new RectoString(
            new Rectangle(882, 52, 157, 43),
            "PASSED");

        public Rectangle OurScoreLocation => new Rectangle(1821, 671, 75, 54);

        public Rectangle EnemyScoreLocation => new Rectangle(1821, 352, 75, 54);

        public Rectangle MulliganEmptySpaceLocation => new Rectangle(3, 7, 1883, 169);

        public Rectangle FinishMulliganButton => new Rectangle(755, 1000, 219, 40);

        public Rectangle BoardEmptySpaceLocation => new Rectangle(0, 0, 1500, 480);

        /// <summary>
        /// game over + connection lost, white text
        /// </summary>
        public RectoString ModalDialogButton1 => new RectoString(
            new Rectangle(927 - 2, 594 - 2, 66 + 4, 18 + 4),
            "ACCEPT");

        /// <summary>
        /// your rewards (next + close), white text
        /// </summary>
        public RectoString ModalDialogButton2 => new RectoString(
            new Rectangle(997, 1005, 58, 20),
            "CLOSE");
        
        /// <summary>
        /// your rewards (close only), white text
        /// </summary>
        public RectoString ModalDialogButton3 => new RectoString(
            new Rectangle(931, 1005, 58, 20),
            "CLOSE");
        
        /// <summary>
        /// welcome back, white text
        /// </summary>
        public RectoString ModalDialogButton4 => new RectoString(
            new Rectangle(915, 1007, 91, 19),
            "CONTINUE");

        /// <summary>
        /// PRESS enter to begin, green text
        /// </summary>
        public RectoString ModalDialogButton5 => new RectoString(
            new Rectangle(811, 997, 64, 28),
            "PRESS");

        /// <summary>
        /// Click anywhere to continue, white text
        /// </summary>
        public RectoString ModalDialogButton6 => new RectoString(
            new Rectangle(844, 950, 144, 39),
            "anywhere");
        
        /// <summary>
        /// Main screen - Choose Play between 4 options
        /// </summary>
        public RectoString ModalDialogButton7 => new RectoString(
            new Rectangle(956, 642, 81, 34),
            "PLAY");
        
        /// <summary>
        /// Battle pass nag screen - press fat X
        /// </summary>
        public RectoString ModalDialogButton8 => new RectoString(
            new Rectangle(17, 1031, 37, 17),
            "VIEW"); // 1862 58 X
        
        public RectoString ModalDialogButton9 => new RectoString(
            new Rectangle(1, 1, 1, 1),
            "N/A");
        
        public RectoString ModalDialogButton10 => new RectoString(
            new Rectangle(1, 1, 1, 1),
            "N/A");
        
        public RectoString ModalDialogButton11 => new RectoString(
            new Rectangle(1, 1, 1, 1),
            "N/A");
        
        public Rectangle LeaderLocation => new Rectangle(28, 696, 134, 152);

        public RectoColor LeaderChargeOn => new RectoColor(
            new Rectangle(58, 822, 5, 5),
            Color.FromArgb(128, 25, 9), 50, 24);

        public RectoColor LeaderChargeOff => new RectoColor(
            new Rectangle(58, 822, 5, 5),
            Color.FromArgb(75, 18, 10), 50, 24);

        public Rectangle LeaderChargeLocationBig => new Rectangle(33, 902, 51, 51);

        public Rectangle BoardPlusHandArea => new Rectangle(360, 504, 1216, 576);

        public Rectangle MulliganArea => new Rectangle(575, 186, 1225, 709);

        public Rectangle PickCardArea => new Rectangle(390, 220, 1120, 640);

        public Rectangle BoardLocation => new Rectangle(272, 515, 1446, 396);

        public Rectangle HandLocation => new Rectangle(387, 903, 1141, 177);

        public Rectangle OurMeleeRowLocation => new Rectangle(384, 509, 1103, 182);

        public Rectangle OurRangedRowLocation => new Rectangle(528, 714, 969, 186);

        public Rectangle EndGameResultLocation => new Rectangle(856, 52, 210, 57);

        public Rectangle EndGameStreakLocation => new Rectangle(737, 52, 447, 57);

        public Rectangle EndGameGgBtnLocation => new Rectangle(925, 848, 66, 53);

        public Rectangle EndGameCloseBtnLocation => new Rectangle(914, 1010, 16, 39);

        public Rectangle MustPlayCard => new Rectangle(1825, 523, 70, 35);

        public Rectangle EmoteButton => new Rectangle(200, 1003, 19, 14);

        public Rectangle Emote1GoingDown => new Rectangle(500, 386, 152, 21);

        public Rectangle Emote2WatchThis => new Rectangle(503, 511, 88, 20);

        public Rectangle Emote3BadMove => new Rectangle(570, 631, 78, 19);

        public Rectangle Emote4WellPlayed => new Rectangle(1272, 385, 91, 20);

        public Rectangle Emote5HurryUp => new Rectangle(1331, 511, 68, 21);

        public Rectangle Emote6Thanks => new Rectangle(1270, 632, 59, 18);

        public Rectangle EnemyTurnDetection => new Rectangle(1811, 510, 69, 62);
    }
}
