using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using gwent_daily_reborn.Model.Cards;
using gwent_daily_reborn.Model.GameInfo;
using gwent_daily_reborn.Model.Helpers.Tooltip;
using gwent_daily_reborn.Model.NN;
using gwent_daily_reborn.Model.Recognition.ScreenShotManager;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Recognition
{
    internal class Recognition : IRecognition
    {
        private readonly Image<Bgra, byte> Detect_endgame_defeat;
        private readonly Image<Bgra, byte> Detect_endgame_draw;
        private readonly Image<Bgra, byte> Detect_endgame_streak;
        private readonly Image<Bgra, byte> Detect_endgame_victory;
        private readonly Image<Bgra, byte> Usurper_lock;
        private readonly Image<Bgra, byte> Detect_Enemy_Turn;
        private readonly Image<Bgra, byte> Detect_Must_Play_Card;

        public Recognition()
        {
            var alias = Hardware.Height + "p";
            try
            {
                Detect_endgame_defeat = new Image<Bgra, byte>($@"images\{alias}\Detect_endgame_defeat.png");
                Detect_endgame_draw = new Image<Bgra, byte>($@"images\{alias}\Detect_endgame_draw.png");
                Detect_endgame_victory = new Image<Bgra, byte>($@"images\{alias}\Detect_endgame_victory.png");
                Detect_endgame_streak = new Image<Bgra, byte>($@"images\{alias}\Detect_endgame_streak.png");
                Usurper_lock = new Image<Bgra, byte>($@"images\{alias}\Usurper_lock.png");
                Detect_Enemy_Turn = new Image<Bgra, byte>($@"images\{alias}\Detect_Enemy_Turn.png");
                Detect_Must_Play_Card = new Image<Bgra, byte>($@"images\{alias}\Detect_must_play_card.png");
            }
            catch (Exception)
            {
                throw new ApplicationException($"Cant load images for {alias} resolution");
            }
        }

        private IHardwareConstants Hardware { get; } = Services.Container.GetInstance<IHardwareConstants>();
        private IOcr Ocr { get; } = Services.Container.GetInstance<IOcr>();
        private IImageCompare ImageCompare { get; } = Services.Container.GetInstance<IImageCompare>();
        private ICardFactory CardFactory { get; } = Services.Container.GetInstance<ICardFactory>();
        private IHand Hand { get; } = Services.Container.GetInstance<IHand>();
        private IBoard Board { get; } = Services.Container.GetInstance<IBoard>();
        private IMulligan Mulligan { get; } = Services.Container.GetInstance<IMulligan>();
        private IExtra Extra { get; } = Services.Container.GetInstance<IExtra>();

        private bool NeedToWaitMulligan { get; set; } = true;

        /// <summary>
        ///     Here we detect game state and analyze screen
        /// </summary>
        public Info Update()
        {
            Reset();
            var info = Info.DoNothing;

            if (!UpdateExtraDataParallel().Result)
                return info;

            if (CloseModalDialog(ref info))
            {
            }
            else if (StuckInComplexAction(ref info))
            {
            }
            else if (DetectHandPlusBoard(ref info))
            {
                NeedToWaitMulligan = true;
            }
            else if (DetectMulligan(ref info))
            {
            }
            else if (PickCard(ref info))
            {
            }
            else if (StartGame(ref info))
            {
            }
            else if (EndGame(ref info))
            {
            }
            else if (EnemyTurn(ref info))
            {
            }
            return info;
        }

        private bool EnemyTurn(ref Info info)
        {
            if (Extra.EnemyTurn == true)
            {
                info = Info.EnemyTurn;
                return true;
            }
            return false;
        }

        private bool StuckInComplexAction(ref Info info)
        {
            if (Extra.UndoAction == true)
            {
                info = Info.Stuck;
                return true;
            }
            return false;
        }

        private bool CloseModalDialog(ref Info info)
        {
            if (Extra.IsModalDialog1Opened == true)
            {
                info = Info.CloseModalDialog1;
                return true;
            }
            if (Extra.IsModalDialog2Opened == true)
            {
                info = Info.CloseModalDialog2;
                return true;
            }
            if (Extra.IsModalDialog3Opened == true)
            {
                info = Info.CloseModalDialog3;
                return true;
            }
            if (Extra.IsModalDialog4Opened == true)
            {
                info = Info.CloseModalDialog4;
                return true;
            }
            if (Extra.IsModalDialog5Opened == true)
            {
                info = Info.CloseModalDialog5;
                return true;
            }
            if (Extra.IsModalDialog6Opened == true)
            {
                info = Info.CloseModalDialog6;
                return true;
            }
            if (Extra.IsModalDialog7Opened == true)
            {
                info = Info.CloseModalDialog7;
                return true;
            }
            return false;
        }

        private bool EndGame(ref Info info)
        {
            if (Extra.IsEndGame == true)
            {
                info = Info.GiveGG;
                return true;
            }
            return false;
        }

        private bool StartGame(ref Info info)
        {
            if (Extra.NeedStartGame == true)
            {
                info = Info.StartNewGame;
                return true;
            }
            return false;
        }

        private async Task<bool> UpdateExtraDataParallel()
        {
            var ssManager = Services.Container.GetInstance<IScreenShotManager>();
            ssManager.UpdateImage();
            Ocr.SetImageGlobal(ssManager.CloneImage());

            #region Create tasks

            var isMulligan = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.MulliganDetection.Rectangle).Convert<Gray, byte>();
                PreProcessText(image, 100);
                return Ocr.AreSame(
                    Hardware.MulliganDetection.Text,
                    image);
            });
            var isOurTurn = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.OurTurnDetection.Rectangle).Convert<Gray, byte>();
                PreProcessText(image, 192);
                //image.Save("ocr.bmp");
                return Ocr.AreSame(
                    Hardware.OurTurnDetection.Text,
                    image);
            });
            var mustPlayCard = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.MustPlayCard);
                return ImageCompare.AreSame(image, Detect_Must_Play_Card, 0.5);
            });
            var isLeaderOn = Task.Run(() =>
            {
                var leaderChargeScreen = ssManager.CloneImage(Hardware.LeaderChargeOn.Rectangle);
                var howManyToCheck = leaderChargeScreen.Width * leaderChargeScreen.Height * Hardware.LeaderChargeOn.Percent / 100;
                var result = ImageCompare.SearchForPixel(
                    leaderChargeScreen,
                    Hardware.LeaderChargeOn.Color,
                    Hardware.LeaderChargeOn.ColorVarience,
                    howManyToCheck);
                //Services.Container.GetInstance<ITooltip>().Show($"isLeaderOn = {result}");
                return result;
            });
            var isLeaderOff = Task.Run(() =>
            {
                var leaderChargeScreen = ssManager.CloneImage(Hardware.LeaderChargeOff.Rectangle);
                var howManyToCheck = leaderChargeScreen.Width * leaderChargeScreen.Height * Hardware.LeaderChargeOff.Percent / 100;
                var result = ImageCompare.SearchForPixel(
                    leaderChargeScreen,
                    Hardware.LeaderChargeOff.Color,
                    Hardware.LeaderChargeOff.ColorVarience,
                    howManyToCheck);
                //Services.Container.GetInstance<ITooltip>().Show($"isLeaderOff = {result}");
                return result;
            });
            var isEnemyPassed = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.EnemyPassedDetection.Rectangle).Convert<Gray, byte>();
                PreProcessText(image, 127);
                return Ocr.AreSame(Hardware.EnemyPassedDetection.Text, image);
            });
            var isEndTurn = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.EndTurnDetection.Rectangle).Convert<Gray, byte>();
                PreProcessText(image, 192);
                //image.Save("ocr.bmp");
                return Ocr.AreSame(Hardware.EndTurnDetection.Text, image);
            });
            var ourScore = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.OurScoreLocation).Convert<Gray, byte>();
                PreProcessText(image, 127);
                return Ocr.GetNumber(image);
            });
            var enemyScore = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.EnemyScoreLocation).Convert<Gray, byte>();
                PreProcessText(image, 127);
                return Ocr.GetNumber(image);
            });
            var roundStatus = Task.Run(() =>
            {
                var enemyCrown = ssManager.CloneImage(Hardware.EnemyHalfCrown.Rectangle);
                var ourCrown = ssManager.CloneImage(Hardware.OurHalfCrown.Rectangle);
                return UpdateRoundStatus(enemyCrown, ourCrown);
            });
            var isPickCard = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.PickCardDetection.Rectangle).Convert<Gray, byte>();
                PreProcessText(image, 127);
                return Ocr.AreSame(
                    Hardware.PickCardDetection.Text, image);
            });
            var needStartGame = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.StartGameDetection.Rectangle).Convert<Gray, byte>();
                PreProcessText(image, 127);
                return Ocr.AreSame(
                    Hardware.StartGameDetection.Text, image);
            });
            var isModalDialog1Opened = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.ModalDialogButton1.Rectangle).Convert<Gray, byte>();
                PreProcessText(image, 127);
                return Ocr.AreSame(
                    Hardware.ModalDialogButton1.Text, image);
            });
            var isModalDialog2Opened = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.ModalDialogButton2.Rectangle).Convert<Gray, byte>();
                PreProcessText(image, 127);
                return Ocr.AreSame(
                    Hardware.ModalDialogButton2.Text, image);
            });
            var isModalDialog3Opened = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.ModalDialogButton3.Rectangle).Convert<Gray, byte>();
                PreProcessText(image, 127);
                return Ocr.AreSame(
                    Hardware.ModalDialogButton3.Text, image);
            });
            var isModalDialog4Opened = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.ModalDialogButton4.Rectangle).Convert<Gray, byte>();
                PreProcessText(image, 127);
                return Ocr.AreSame(
                    Hardware.ModalDialogButton4.Text, image);
            });
            var isModalDialog5Opened = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.ModalDialogButton5.Rectangle).Convert<Gray, byte>();
                return Ocr.AreSame(
                    Hardware.ModalDialogButton5.Text, image);
            });
            var isModalDialog6Opened = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.ModalDialogButton6.Rectangle).Convert<Gray, byte>();
                PreProcessText(image, 127);
                return Ocr.AreSame(
                    Hardware.ModalDialogButton6.Text, image);
            });
            var isModalDialog7Opened = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.ModalDialogButton7.Rectangle).Convert<Gray, byte>();
                PreProcessText(image, 127);
                return Ocr.AreSame(
                    Hardware.ModalDialogButton7.Text, image);
            });
            var isEndGameDefeat = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.EndGameResultLocation);
                return ImageCompare.AreSame(image, Detect_endgame_defeat);
            });
            var isEndGameDraw = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.EndGameResultLocation);
                return ImageCompare.AreSame(image, Detect_endgame_draw);
            });
            var isEndGameVictory = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.EndGameResultLocation);
                return ImageCompare.AreSame(image, Detect_endgame_victory);
            });
            var isEndGameStreak = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.EndGameStreakLocation);
                return ImageCompare.AreSame(image, Detect_endgame_streak);
            });
            var undoAction = Task.Run(() =>
            {
                var undoScreen = ssManager.CloneImage(Hardware.UndoAction.Rectangle);
                var howManyToCheck = undoScreen.Width * undoScreen.Height * Hardware.UndoAction.Percent / 100;
                return ImageCompare.SearchForPixel(
                    undoScreen,
                    Hardware.UndoAction.Color,
                    Hardware.UndoAction.ColorVarience,
                    howManyToCheck);
            });
            var enemyTurn = Task.Run(() =>
            {
                var image = ssManager.CloneImage(Hardware.EnemyTurnDetection);
                return ImageCompare.AreSame(image, Detect_Enemy_Turn);
            });

            #endregion

            await Task.WhenAll(
                isMulligan, isOurTurn, isLeaderOn, isLeaderOff,
                isEnemyPassed, isEndTurn, ourScore, enemyScore,
                roundStatus, isPickCard, needStartGame, isModalDialog1Opened,
                isModalDialog2Opened, isModalDialog3Opened, isModalDialog4Opened,
                isModalDialog5Opened, isModalDialog6Opened, isModalDialog7Opened,
                isEndGameDefeat, isEndGameDraw, isEndGameVictory, isEndGameStreak, 
                mustPlayCard, undoAction, enemyTurn).ConfigureAwait(false);

            Extra.Update(
                isMulligan.Result, isOurTurn.Result, isLeaderOn.Result, isLeaderOff.Result,
                isEnemyPassed.Result, isEndTurn.Result, ourScore.Result, enemyScore.Result,
                roundStatus.Result, isPickCard.Result, needStartGame.Result,
                isEndGameDefeat.Result || isEndGameDraw.Result || isEndGameVictory.Result || isEndGameStreak.Result,
                isModalDialog1Opened.Result, 
                isModalDialog2Opened.Result, isModalDialog3Opened.Result, isModalDialog4Opened.Result,
                isModalDialog5Opened.Result, isModalDialog6Opened.Result, isModalDialog7Opened.Result,
                mustPlayCard.Result, undoAction.Result, enemyTurn.Result);

            return true;
        }

        /// <summary>
        ///     Prepares image for OCR
        /// </summary>
        /// <param name="image">image with WHITE text on DARK background</param>
        /// <param name="threshold">127 is good starting value</param>
        private static void PreProcessText(IOutputArray image, int threshold)
        {
            CvInvoke.Threshold(image, image, threshold, 255, ThresholdType.BinaryInv);
        }

        private Rounds UpdateRoundStatus(Image<Bgra, byte> enemyCrown, Image<Bgra, byte> ourCrown)
        {
            var howManyToCheck = enemyCrown.Width * enemyCrown.Height / 20;
            var isEnemyCrownFull = ImageCompare.SearchForPixel(
                enemyCrown,
                Hardware.EnemyHalfCrown.Color,
                Hardware.EnemyHalfCrown.ColorVarience,
                howManyToCheck);
            var isOurCrownFull = ImageCompare.SearchForPixel(
                ourCrown,
                Hardware.OurHalfCrown.Color,
                Hardware.OurHalfCrown.ColorVarience,
                howManyToCheck);
            if (isEnemyCrownFull)
                return isOurCrownFull ? Rounds.Draw : Rounds.EnemyLead;
            return isOurCrownFull ? Rounds.WeLead : Rounds.FirstRound;
        }

        private void Reset()
        {
            Hand.Reset();
            Board.Reset();
            Mulligan.Reset();
        }

        private bool PickCard(ref Info info)
        {
            if (Extra.IsPickCard != true)
                return false;
            var image = Services.Container.GetInstance<IScreenShotManager>().CloneImage(Hardware.PickCardArea);
            var items = Services.Container.GetInstance<NeuralNet>().GetItems(image.Bitmap);
            var cards = CardFactory.Create(items, new Point(Hardware.PickCardArea.X, Hardware.PickCardArea.Y));
            Mulligan.Update(cards);
            info = Info.PickCard;
            return true;
        }

        private bool DetectMulligan(ref Info info)
        {
            if (Extra.IsMulligan != true)
                return false;
            if (NeedToWaitMulligan)
            {
                NeedToWaitMulligan = false;
                Thread.Sleep(1500); // Mulligan animation is slow
            }
            var image = Services.Container.GetInstance<IScreenShotManager>().CloneImage(Hardware.MulliganArea);
            var items = Services.Container.GetInstance<NeuralNet>().GetItems(image.Bitmap);
            var cards = CardFactory.Create(items, new Point(Hardware.MulliganArea.X, Hardware.MulliganArea.Y));
            Mulligan.Update(cards);
            info = Mulligan.Count() < 3 ? Info.DoNothing : Info.Mulligan;
            return true;
        }

        private bool DetectHandPlusBoard(ref Info info)
        {
            if (Extra.IsOurTurn == null || Extra.IsLeaderOn == null || Extra.IsLeaderOff == null)
                return false;
            if (Extra.IsLeaderOn == Extra.IsLeaderOff)
                return false;
            if (Extra.IsOurTurn == false && Extra.IsEndTurn == false && Extra.MustPlayCard == false)
                return false;
            var image = Services.Container.GetInstance<IScreenShotManager>().CloneImage(Hardware.BoardPlusHandArea);
            var items = Services.Container.GetInstance<NeuralNet>().GetItems(image.Bitmap);
            var cards = CardFactory.Create(items, new Point(Hardware.BoardPlusHandArea.X, Hardware.BoardPlusHandArea.Y));
            Hand.Update(cards, Extra.IsLeaderOn == true && Extra.IsLeaderOff == false);
            Board.Update(cards);
            info = Extra.IsEndTurn == true ? Info.EndTurn : Info.ReadyToPlay;
            // sometimes we must play a card and cant pass
            if (Extra.MustPlayCard == true)
            {
                info = Info.MustPlayCard;
            }
            return true;
        }
    }
}
