using System.Drawing;

namespace gwent_daily_reborn.Model.Settings
{
    /// <summary>
    /// Expected text with limiting bonds
    /// </summary>
    internal class RectoString
    {
        public RectoString(Rectangle rectangle, string text)
        {
            Rectangle = rectangle;
            Text = text;
        }

        public Rectangle Rectangle { get; }
        public string Text { get; }
    }

    /// <summary>
    /// Expected color with limiting bonds 
    /// </summary>
    internal class RectoColor
    {
        internal RectoColor(Rectangle rectangle, Color color, int percent, int colorVarience)
        {
            Rectangle = rectangle;
            Color = color;
            Percent = percent;
            ColorVarience = colorVarience;
        }

        public Rectangle Rectangle { get; }

        public Color Color { get; }

        /// <summary>
        /// Requred percent of pixels with required color
        /// </summary>
        public int Percent { get; }

        /// <summary>
        /// Maximum varience (equlid distance)
        /// </summary>
        public int ColorVarience { get; }
    }

    internal interface IHardwareConstants
    {
        #region Global

        int Width { get; }

        int Height { get; }

        float CardDetectionMinimalConfidenceLevel { get; }

        #endregion

        #region Detection

        Rectangle EnemyTurnDetection { get; }

        /// <summary>
        ///     Obsolete. Use image instead
        /// </summary>
        Rectangle MustPlayCard { get; }

        /// <summary>
        ///     Location with mouse icon. Pressing right button will cancel action
        /// </summary>
        RectoColor UndoAction { get; }

        /// <summary>
        ///     Our left half crown location
        /// </summary>
        RectoColor OurHalfCrown { get; }

        /// <summary>
        ///     Enemy left half crown location
        /// </summary>
        RectoColor EnemyHalfCrown { get; }

        RectoString OurTurnDetection { get; }

        RectoString EndTurnDetection { get; }

        RectoString MulliganDetection { get; }

        RectoString PickCardDetection { get; }

        /// <summary>
        ///     Object to look for starting game stage
        /// </summary>
        RectoColor StartGameDetection { get; }

        /// <summary>
        ///     Play -> Classic union zone
        /// </summary>
        Rectangle StartGameClickLocation { get; }

        RectoString EnemyPassedDetection { get; }

        #endregion

        #region Zones
        
        /// <summary>
        /// Click and BM enemy
        /// </summary>
        Rectangle EmoteButton { get; }

        Rectangle Emote1GoingDown { get; }

        Rectangle Emote2WatchThis { get; }

        Rectangle Emote3BadMove { get; }

        Rectangle Emote4WellPlayed { get; }

        Rectangle Emote5HurryUp { get; }

        Rectangle Emote6Thanks { get; }

        Rectangle OurScoreLocation { get; }

        Rectangle EnemyScoreLocation { get; }

        /// <summary>
        ///     We need to click on some empty space to reset selected card
        /// </summary>
        Rectangle MulliganEmptySpaceLocation { get; }

        /// <summary>
        ///     Button to finish redraw stage
        /// </summary>
        Rectangle FinishMulliganButton { get; }

        /// <summary>
        ///     Empty space on board to clear selection
        /// </summary>
        Rectangle BoardEmptySpaceLocation { get; }

        /// <summary>
        ///     When enemy concedes
        /// </summary>
        RectoString ModalDialogButton1 { get; }

        /// <summary>
        ///     When quest completed
        /// </summary>
        RectoString ModalDialogButton2 { get; }
        
        /// <summary>
        ///     When quest completed
        /// </summary>
        RectoString ModalDialogButton3 { get; }
        
        /// <summary>
        ///     Reserved
        /// </summary>
        RectoString ModalDialogButton4 { get; }

        #endregion

        #region Leader

        /// <summary>
        ///     Place to click for leader use
        /// </summary>
        Rectangle LeaderLocation { get; }

        RectoColor LeaderChargeOn { get; }

        RectoColor LeaderChargeOff { get; }

        /// <summary>
        ///     LeaderCharge big for checking usurper lock (image compare)
        /// </summary>
        Rectangle LeaderChargeLocationBig { get; }

        #endregion

        #region Coordinates for taking screenshots

        /// <summary>
        ///     Coordinates for taking screenshots
        /// </summary>
        Rectangle BoardPlusHandArea { get; }

        /// <summary>
        ///     Coordinates for taking screenshots
        /// </summary>
        Rectangle MulliganArea { get; }

        /// <summary>
        ///     Coordinates for taking screenshots
        /// </summary>
        Rectangle PickCardArea { get; }

        #endregion

        #region Zones to detect card place

        /// <summary>
        ///     Zones to detect card place
        /// </summary>
        Rectangle BoardLocation { get; }

        /// <summary>
        ///     Zones to detect card place
        /// </summary>
        Rectangle HandLocation { get; }

        /// <summary>
        ///     Zones to detect card place
        /// </summary>
        Rectangle OurMeleeRowLocation { get; }

        /// <summary>
        ///     Zones to detect card place
        /// </summary>
        Rectangle OurRangedRowLocation { get; }

        #endregion

        #region Endgame

        /// <summary>
        ///     Defeat / draw / victory text location
        /// </summary>
        Rectangle EndGameResultLocation { get; }

        /// <summary>
        ///     Streak text location
        /// </summary>
        Rectangle EndGameStreakLocation { get; }

        /// <summary>
        ///     GG button
        /// </summary>
        Rectangle EndGameGgBtnLocation { get; }

        /// <summary>
        ///     Close end game screen button
        /// </summary>
        Rectangle EndGameCloseBtnLocation { get; }

        RectoString ModalDialogButton5 { get; }
        RectoString ModalDialogButton6 { get; }
        RectoString ModalDialogButton7 { get; }

        #endregion

    }
}
