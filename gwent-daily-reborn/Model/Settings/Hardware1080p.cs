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
            new Rectangle(1828, 527, 64, 26),
            "PASS");

        public RectoString EndTurnDetection => new RectoString(
            new Rectangle(1834, 512, 52, 27),
            "END");

        public RectoString MulliganDetection => new RectoString(
            new Rectangle(778, 1010, 59, 19),
            "FINISH");

        public RectoString PickCardDetection => new RectoString(
            new Rectangle(830, 33, 93, 45),
            "PICK");

        public RectoColor StartGameDetection => new RectoColor(
            new Rectangle(1856, 999, 7, 20),
            Color.FromArgb(176, 134, 80), 20, 32);

        public Rectangle StartGameClickLocation => new Rectangle(812, 342, 151, 338);

        public RectoString EnemyPassedDetection => new RectoString(
            new Rectangle(882, 52, 157, 43),
            "PASSED");

        public Rectangle OurScoreLocation => new Rectangle(1821, 671, 75, 54);

        public Rectangle EnemyScoreLocation => new Rectangle(1821, 352, 75, 54);

        public Rectangle MulliganEmptySpaceLocation => new Rectangle(3, 7, 1883, 169);

        public Rectangle FinishMulliganButton => new Rectangle(755, 1000, 219, 40);

        public Rectangle BoardEmptySpaceLocation => new Rectangle(0, 0, 1500, 480);

        public RectoString ModalDialogButton1 => new RectoString(
            new Rectangle(927 - 2, 594 - 2, 66 + 4, 18 + 4),
            "ACCEPT"); // game over + connection lost

        public RectoString ModalDialogButton2 => new RectoString(
            new Rectangle(997, 1005, 58, 20),
            "CLOSE"); // your rewards (next + close)
        
        public RectoString ModalDialogButton3 => new RectoString(
            new Rectangle(931, 1005, 58, 20),
            "CLOSE"); // your rewards (close only)
        
        public RectoString ModalDialogButton4 => new RectoString(
            new Rectangle(927, 597, 66, 17),
            "XXXXXXX"); // XXXXXXX

        public Rectangle LeaderLocation => new Rectangle(28, 696, 134, 152);

        public RectoColor LeaderChargeOn => new RectoColor(
            new Rectangle(35, 752, 5, 5),
            Color.FromArgb(126, 24, 8), 50, 24);

        public RectoColor LeaderChargeOff => new RectoColor(
            new Rectangle(35, 752, 5, 5),
            Color.FromArgb(75, 16, 9), 50, 24);

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

        public Rectangle EnemyTurnDetection => new Rectangle(1824, 519, 73, 35);
    }
}
