using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using gwent_daily_reborn.Model.Cards;
using gwent_daily_reborn.Model.Cards.Extra;
using gwent_daily_reborn.Model.Cards.Leader;
using gwent_daily_reborn.Model.Cards.Monster;
using gwent_daily_reborn.Model.Cards.Neutral;
using gwent_daily_reborn.Model.Control.BotTasks;
using gwent_daily_reborn.Model.GameInfo;
using gwent_daily_reborn.Model.GameMemory;
using gwent_daily_reborn.Model.Helpers;
using gwent_daily_reborn.Model.Helpers.Keyboard;
using gwent_daily_reborn.Model.Recognition;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Control.Strategy
{
    /// <summary>
    ///     Handles deck specific logic
    /// </summary>
    internal class MonsterStrategy : IStrategy, IResetAfterGame
    {
        private IHardwareConstants Hardware { get; } = Services.Container.GetInstance<IHardwareConstants>();
        private IHand Hand { get; } = Services.Container.GetInstance<IHand>();
        private IBoard Board { get; } = Services.Container.GetInstance<IBoard>();
        private IMulligan Mulligan { get; } = Services.Container.GetInstance<IMulligan>();
        private IExtra Extra { get; } = Services.Container.GetInstance<IExtra>();
        private IGameMemory GameMemory { get; } = Services.Container.GetInstance<IGameMemory>();

        private IEnumerable<Type> LeaderBuffList { get; } = new List<Type>
        {
            typeof(Werewolf),
            typeof(AlphaWerewolf),
            typeof(Plumard),
            typeof(Cyclops),
            typeof(Brewess),
            typeof(Whispess),
            typeof(Weavess),
            typeof(OldSpeartip),
            typeof(PrimordialDao),
            typeof(OldSpeartipAsleep),
            typeof(IceGiant),
            typeof(Griffin),
            typeof(Katakan),
            typeof(WildHuntRider),
            typeof(Golyat),
        };

        private IEnumerable<Type> FromHighToLowStr { get; } = new List<Type>
        {
            typeof(Golyat), //10
            typeof(OldSpeartip), //12
            typeof(OldSpeartipAsleep), //9
            typeof(PrimordialDao), //9
            typeof(Griffin), //8
            typeof(IceGiant), //7
            typeof(Katakan), //6
            typeof(Brewess), //6
            typeof(Weavess), //5
            typeof(CelaenoHarpy), //5
            typeof(Cyclops), //4
            typeof(Whispess), //4
            typeof(Werewolf), //4
            typeof(AlphaWerewolf), //4
            typeof(WildHuntRider), //3
            typeof(Plumard) //3
        };

        /// <summary>
        ///     Play a card according to our strategy
        /// </summary>
        /// <param name="tasks">Returns list of simple actions (ex. mouse move, left click, end turn)</param>
        /// <param name="info"></param>
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        public bool Play(out ICollection<IBotTask> tasks, Info info)
        {
            switch (info)
            {
                case Info.ReadyToPlay:
                    return ReadyToPlayLogic(out tasks);
                case Info.MustPlayCard:
                    return MustPlayLogic(out tasks);
                case Info.EndTurn:
                    return EndTurnLogic(out tasks);
                case Info.Mulligan:
                    return MulliganLogic(out tasks);
                case Info.PickCard:
                    return PickCardLogic(out tasks);
                case Info.DoNothing:
                case Info.ActivateGwentWindow:
                case Info.GiveGG:
                case Info.StartNewGame:
                case Info.CloseModalDialog1:
                case Info.CloseModalDialog2:
                case Info.CloseModalDialog3:
                case Info.CloseModalDialog4:
                case Info.Stuck:
                    return DoNothing(out tasks);
                default:
                    throw new Exception($"{info} is not implemented");
            }
        }

        /// <summary>
        ///     Returns current potential hand value
        /// </summary>
        private int GetHandValue()
        {
            var value = Hand.GetCards().Sum(card => card.Value);
            if (Hand.Contain(typeof(Ghoul)))
            {
                if (GameMemory.InGraveyard(typeof(IceGiant)))
                    value += 7;
                else if (GameMemory.InGraveyard(typeof(Cyclops)))
                    value += 5;
                else if (GameMemory.InGraveyard(typeof(Werewolf)) ||
                         GameMemory.InGraveyard(typeof(AlphaWerewolf)) ||
                         GameMemory.InGraveyard(typeof(NekkerWarrior)) ||
                         GameMemory.InGraveyard(typeof(WildHuntRider)))
                    value += 4;
            }
            if (Hand.Contain(typeof(Ozzrel)))
            {
                if (GameMemory.InGraveyard(typeof(OldSpeartip)))
                    value += 12;
                else if (GameMemory.InGraveyard(typeof(CountCaldwell)) ||
                         GameMemory.InGraveyard(typeof(Golyat)))
                    value += 10;
                else if (GameMemory.InGraveyard(typeof(OldSpeartipAsleep)) ||
                         GameMemory.InGraveyard(typeof(PrimordialDao)))
                    value += 9;
                else if (GameMemory.InGraveyard(typeof(IceGiant)))
                    value += 7;
            }
            if (Hand.Contain(typeof(WeavessIncantation)))
            {
                if (Hand.Contain(typeof(OldSpeartip)))
                    value += 12;
                else if (Hand.Contain(typeof(CountCaldwell)) ||
                         Hand.Contain(typeof(Golyat)))
                    value += 10;
                else if (Hand.Contain(typeof(OldSpeartipAsleep)) ||
                         Hand.Contain(typeof(PrimordialDao)))
                    value += 9;
                else if (Hand.Contain(typeof(IceGiant)))
                    value += 7;
            }
            return value;
        }

        private bool PickCardLogic(out ICollection<IBotTask> tasks)
        {
            tasks = new List<IBotTask>();
            if (LeaderBuffMode)
            {
                LeaderBuffMode = false;
                tasks.Add(new TooltipTask("Leader mode..."));
                foreach (var cardType in LeaderBuffList)
                    if (Mulligan.Contain(cardType))
                    {
                        var zone = Mulligan.GetCard(cardType).Area;
                        tasks.Add(new TooltipTask("Buffing..."));
                        tasks.Add(new MouseMoveTask(zone));
                        tasks.Add(new LeftMouseClick());
                        tasks.Add(new SleepTask(1000));
                        return true;
                    }
            }
            else
            {
                foreach (var cardType in FromHighToLowStr)
                    if (Mulligan.Contain(cardType))
                    {
                        if (GhoulMode || OzzrelMode)
                        {
                            GameMemory.DeleteFromGraveyard(cardType);
                        }
                        if (WeavessIncantationMode)
                        {
                            GameMemory.AddToGraveyard(cardType);
                        }

                        GhoulMode = false;
                        OzzrelMode = false;
                        WeavessIncantationMode = false;

                        var zone = Mulligan.GetCard(cardType).Area;
                        tasks.Add(new TooltipTask("Consuming..."));
                        tasks.Add(new MouseMoveTask(zone));
                        tasks.Add(new LeftMouseClick());
                        tasks.Add(new SleepTask(4000));
                        return true;
                    }
            }
            tasks.Add(new TooltipTask("Choosing random target..."));
            tasks.Add(new KeyboardTask(Messaging.VKeys.Left));
            tasks.Add(new KeyboardTask(Messaging.VKeys.Right));
            tasks.Add(new KeyboardTask(Messaging.VKeys.Enter));
            tasks.Add(new KeyboardTask(Messaging.VKeys.Enter));
            tasks.Add(new SleepTask(1000));
            return true;
        }

        private static bool DoNothing(out ICollection<IBotTask> tasks)
        {
            tasks = new List<IBotTask>();
            return true;
        }

        private bool NotRound1() => GameMemory.GetGy().Any() || GameMemory.GetPlayed().Any();

        private bool IsRound1() => !NotRound1();

        private bool MulliganLogic(out ICollection<IBotTask> tasks)
        {
            tasks = new List<IBotTask>();

            #region Replace some pairs

            if (Mulligan.Count(typeof(WildHuntRider)) == 2)
            {
                var card = Mulligan.GetCard(typeof(WildHuntRider));
                return AddMulliganTask(tasks, card, "Replace 2 riders");
            }

            if (Mulligan.Count(typeof(Foglet)) == 2)
            {
                var card = Mulligan.GetCard(typeof(Foglet));
                return AddMulliganTask(tasks, card, "Replace 2 foglets");
            }

            if (Mulligan.Count(typeof(Werewolf)) == 2)
            {
                var card = Mulligan.GetCard(typeof(Werewolf));
                return AddMulliganTask(tasks, card, "Replace 2 werewolfs");
            }

            if (Mulligan.Count(typeof(CelaenoHarpy)) == 2)
            {
                var card = Mulligan.GetCard(typeof(CelaenoHarpy));
                return AddMulliganTask(tasks, card, "Replace 2 celaeno");
            }

            if (IsRound1())
                if (Mulligan.Count(typeof(Nekker)) == 2)
                {
                    var card = Mulligan.GetCard(typeof(Nekker));
                    return AddMulliganTask(tasks, card, "Replace 2 nekkers");
                }

            #endregion

            #region Already tried to thin foglet

            if (GameMemory.InGraveyard(typeof(Foglet)) &&
                Hand.Contain(typeof(Foglet)))
            {
                var card = Mulligan.GetCard(typeof(Foglet));
                return AddMulliganTask(tasks, card, "Already tried to thin foglet");
            }

            #endregion

            #region Replace ozzrel and ghouls

            if (IsRound1())
            {
                if (Mulligan.Contain(typeof(Ghoul)))
                {
                    var card = Mulligan.GetCard(typeof(Ghoul));
                    return AddMulliganTask(tasks, card, "Don't need ghoul R1");
                }

                if (Mulligan.Contain(typeof(Ozzrel)))
                {
                    var card = Mulligan.GetCard(typeof(Ozzrel));
                    return AddMulliganTask(tasks, card, "Don't need ozzrel R1");
                }
            }

            #endregion

            #region Replace weak card in R2+

            if (NotRound1())
            {
                if (Mulligan.Contain(typeof(Foglet)) &&
                    GameMemory.InGraveyard(typeof(Foglet)))
                {
                    var card = Mulligan.GetCard(typeof(Foglet));
                    return AddMulliganTask(tasks, card, "Don't need Foglet in R2+");
                }
                if (Mulligan.Contain(typeof(Plumard)))
                {
                    var card = Mulligan.GetCard(typeof(Plumard));
                    return AddMulliganTask(tasks, card, "Don't need Plumard in R2+");
                }
                if (Mulligan.Contain(typeof(Nekker)))
                {
                    var card = Mulligan.GetCard(typeof(Nekker));
                    return AddMulliganTask(tasks, card, "Don't need Nekker in R2+");
                }
                if (Mulligan.Contain(typeof(Wyvern)))
                {
                    var card = Mulligan.GetCard(typeof(Wyvern));
                    return AddMulliganTask(tasks, card, "Don't need Wyvern in R2+");
                }
                if (Mulligan.Contain(typeof(NekkerWarrior)))
                {
                    var card = Mulligan.GetCard(typeof(NekkerWarrior));
                    return AddMulliganTask(tasks, card, "Don't need NekkerWarrior in R2+");
                }
                if (Mulligan.Contain(typeof(Brewess)))
                {
                    var card = Mulligan.GetCard(typeof(Brewess));
                    return AddMulliganTask(tasks, card, "Don't need Brewess in R2+");
                }
            }

            #endregion

            tasks.Add(new FinishMulliganTask());
            return false;
        }

        /// <summary>
        ///     Clicks on card and empty space after
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="card"></param>
        /// <param name="msg"></param>
        private bool AddMulliganTask(ICollection<IBotTask> tasks, Card card, string msg)
        {
            tasks.Add(new TooltipTask(msg));
            tasks.Add(new MouseMoveTask(card.Area));
            tasks.Add(new LeftMouseClick());
            tasks.Add(new MouseMoveTask(Hardware.MulliganEmptySpaceLocation));
            tasks.Add(new LeftMouseClick());
            tasks.Add(new SleepTask(1000));
            return true;
        }

        private bool EndTurnLogic(out ICollection<IBotTask> tasks)
        {
            tasks = new List<IBotTask>();

            // buff foglet so it wont die before thin
            if (Board.Contain(typeof(TacticalAdvantage)) &&
                Board.Contain(typeof(Foglet)))
                return Hand.Play(typeof(TacticalAdvantage), out tasks);

            // Tactical advantage 1
            if (Extra.CurrentRound == Rounds.FirstRound &&
                Board.Contain(typeof(TacticalAdvantage)) &&
                !Hand.Contain(typeof(Foglet)) &&
                Hand.Count() <= 7)
                return Hand.Play(typeof(TacticalAdvantage), out tasks);

            // Tactical advantage 2
            if (Extra.CurrentRound == Rounds.FirstRound &&
                Board.Contain(typeof(TacticalAdvantage)) &&
                Hand.Count() <= 6)
                return Hand.Play(typeof(TacticalAdvantage), out tasks);

            tasks.Add(new EndTurn());
            return true;
        }

        private bool MustPlayLogic(out ICollection<IBotTask> tasks)
        {
            tasks = new List<IBotTask>();
            if (Hand.Count() == 0)
                return false;

            if (PlayLogic(ref tasks))
                return true;

            return false;
        }

        private bool ReadyToPlayLogic(out ICollection<IBotTask> tasks)
        {
            tasks = new List<IBotTask>();
            if (Hand.Count() == 0)
                return false;

            if (PassLogic(ref tasks))
                return true;

            if (PlayLogic(ref tasks))
                return true;

            return false;
        }

        private bool PlayLogic(ref ICollection<IBotTask> tasks)
        {
            var leastBusyRow = Board.RangedSize < Board.MeleeSize ? Card.Rows.Ranged : Card.Rows.Melee;

            #region Thin foglet

            // consume
            if (Board.Count(typeof(Foglet)) == 1)
            {
                var foglet = Board.GetCard(typeof(Foglet));
                var placePoint = new Point(foglet.Area.Right, foglet.Y + foglet.Height / 2);
                if (Hand.Contain(typeof(Brewess)))
                    return Hand.Play(typeof(Brewess), out tasks, leastBusyRow, placePoint);
                if (Hand.Contain(typeof(CelaenoHarpy)))
                    return Hand.Play(typeof(CelaenoHarpy), out tasks, leastBusyRow, placePoint);
                if (Hand.Contain(typeof(Cyclops)))
                    return Hand.Play(typeof(Cyclops), out tasks, leastBusyRow, placePoint);
            }
            // place foglet
            if (Hand.Contain(typeof(Foglet)) &&
                (Hand.Contain(typeof(Cyclops)) || Hand.Contain(typeof(Brewess)) || Hand.Contain(typeof(CelaenoHarpy))))
                return Hand.Play(typeof(Foglet), out tasks, Card.Rows.Melee);

            #endregion

            #region Blow brewess/cyclop if we alredy thinned foglet (or its too late)

            if (Extra.CurrentRound == Rounds.Draw ||
                GameMemory.InGraveyard(typeof(Foglet)))
            {
                if (Hand.Contain(typeof(Cyclops)))
                    return Hand.Play(typeof(Cyclops), out tasks);
                if (Hand.Contain(typeof(Brewess)))
                    return Hand.Play(typeof(Brewess), out tasks);
            }

            if (Extra.CurrentRound == Rounds.Draw &&
                Hand.Contain(typeof(Golyat)) &&
                !Hand.Contain(typeof(Katakan)) &&
                !Board.Contain(typeof(Katakan)) &&
                !Hand.Contain(typeof(Foglet)) &&
                !Board.Contain(typeof(Foglet)))
            {
                if (Hand.Contain(typeof(Cyclops)))
                    return Hand.Play(typeof(Cyclops), out tasks);
                if (Hand.Contain(typeof(Brewess)))
                    return Hand.Play(typeof(Brewess), out tasks);
                if (Hand.Contain(typeof(CelaenoHarpy)))
                    return Hand.Play(typeof(CelaenoHarpy), out tasks);
            }

            #endregion

            #region Wild hunt raider

            // Manually adding extra wild hunt rider to played list
            if (Board.Count(typeof(WildHuntRider)) == 2 &&
                GameMemory.GetGy().Count(card => card == typeof(WildHuntRider)) == 1)
                GameMemory.RegisterCard(typeof(WildHuntRider));
            // Play wild hunt raider if we can (easy check)
            if (Extra.EnemyScore <= 4 &&
                Hand.Count(typeof(WildHuntRider)) == 1)
                return Hand.Play(typeof(WildHuntRider), out tasks, Card.Rows.Ranged);

            #endregion

            #region Small thrive

            if (Hand.Contain(typeof(Nekker)))
                return Hand.Play(typeof(Nekker), out tasks, leastBusyRow);
            if (Hand.Contain(typeof(Wyvern)) && Extra.EnemyScore >= 3)
                return Hand.Play(typeof(Wyvern), out tasks, Card.Rows.Melee);
            if (Hand.Contain(typeof(NekkerWarrior)))
                return Hand.Play(typeof(NekkerWarrior), out tasks, leastBusyRow);
            if (Hand.Contain(typeof(AlphaWerewolf)) &&
                HaveOtherImmuneShit())
                return Hand.Play(typeof(AlphaWerewolf), out tasks, leastBusyRow);

            #endregion

            #region Plumard

            // Play 2 plumard if we can
            if (Hand.Contain(typeof(Plumard)) &&
                Extra.EnemyScore >= 3 &&
                Board.Count(typeof(Plumard)) + Hand.Count(typeof(Plumard)) >= 2)
                return Hand.Play(typeof(Plumard), out tasks, leastBusyRow);

            #endregion

            #region Play medium sized threats

            if (Hand.Contain(typeof(Katakan)))
                return Hand.Play(typeof(Katakan), out tasks, leastBusyRow);
            if (Hand.Contain(typeof(WeavessIncantation)))
                if (Hand.Contain(typeof(PrimordialDao)) ||
                    Hand.Contain(typeof(Golyat)) ||
                    Hand.Contain(typeof(OldSpeartipAsleep)) ||
                    Hand.Contain(typeof(OldSpeartip)) ||
                    Hand.Contain(typeof(CountCaldwell)) ||
                    Hand.Contain(typeof(IceGiant)))
                {
                    WeavessIncantationMode = true;
                    return Hand.Play(typeof(WeavessIncantation), out tasks, Card.Rows.Melee);
                }
            if (Hand.Contain(typeof(IceGiant)))
                return Hand.Play(typeof(IceGiant), out tasks, leastBusyRow);
            if (Hand.Contain(typeof(PrimordialDao)))
                return Hand.Play(typeof(PrimordialDao), out tasks, leastBusyRow);
            if (Hand.Contain(typeof(OldSpeartipAsleep)))
                return Hand.Play(typeof(OldSpeartipAsleep), out tasks, leastBusyRow);
            if (Hand.Contain(typeof(OldSpeartip)))
                return Hand.Play(typeof(OldSpeartip), out tasks, leastBusyRow);

            #endregion

            #region Crones

            if (Hand.Contain(typeof(Whispess)) && Extra.EnemyScore >= 3)
                return Hand.Play(typeof(Whispess), out tasks);
            if (Hand.Contain(typeof(Weavess)) && SomethingToBuff())
                return Hand.Play(typeof(Weavess), out tasks, leastBusyRow);

            #endregion

            #region Swallow + Parasyte
            if (Board.Count() >= 2 &&
                Hand.Contain(typeof(Swallow)) &&
                !Board.Contain(typeof(TacticalAdvantage)))
            {
                return Hand.Play(typeof(Swallow), out tasks);
            }
            if (Board.Count() >= 2 &&
                Hand.Contain(typeof(Parasite)) &&
                !Board.Contain(typeof(TacticalAdvantage)))
            {
                return Hand.Play(typeof(Parasite), out tasks);
            }
            #endregion

            #region Consume

            if (Extra.CurrentRound != Rounds.FirstRound &&
                Hand.Contain(typeof(Ghoul)))
                if (GameMemory.InGraveyard(typeof(IceGiant)) ||
                    GameMemory.InGraveyard(typeof(Weavess)) ||
                    GameMemory.InGraveyard(typeof(Whispess)) ||
                    GameMemory.InGraveyard(typeof(Brewess)) ||
                    GameMemory.InGraveyard(typeof(Cyclops)))
                {
                    GhoulMode = true;
                    return Hand.Play(typeof(Ghoul), out tasks, Card.Rows.Melee);
                }

            if (Extra.CurrentRound != Rounds.FirstRound &&
                Hand.Contain(typeof(Ozzrel)))
                if (GameMemory.InGraveyard(typeof(OldSpeartipAsleep)) ||
                    GameMemory.InGraveyard(typeof(OldSpeartip)) ||
                    GameMemory.InGraveyard(typeof(Golyat)) ||
                    GameMemory.InGraveyard(typeof(CountCaldwell)) ||
                    GameMemory.InGraveyard(typeof(PrimordialDao)))
                {
                    OzzrelMode = true;
                    return Hand.Play(typeof(Ozzrel), out tasks, Card.Rows.Ranged);
                }

            #endregion

            #region Leader logic

            if (Extra.CurrentRound != Rounds.FirstRound && 
                Hand.Contain(typeof(WoodlandSpirit)))
            {
                LeaderBuffMode = true;
                return Hand.Play(typeof(WoodlandSpirit), out tasks);
            }
            if (LeaderBuffMode &&
                !Hand.Contain(typeof(WoodlandSpirit)))
            {
                LeaderBuffMode = false;
                foreach (var card in FromHighToLowStr.Reverse())
                    if (Hand.Contain(card))
                        return Hand.Play(card, out tasks);
            }

            #endregion

            #region Katakan->Consume

            if (Board.Contain(typeof(Katakan)))
            {
                var katakan = Board.GetCard(typeof(Katakan));
                var placePoint = new Point(katakan.Area.Right, katakan.Y + katakan.Height / 2);
                if (Hand.Contain(typeof(Brewess)))
                {
                    tasks.Add(new TooltipTask("Katakan->Consume"));
                    return Hand.Play(typeof(Brewess), out tasks, default, placePoint);
                }
                if (Hand.Contain(typeof(CelaenoHarpy)))
                {
                    tasks.Add(new TooltipTask("Katakan->Consume"));
                    return Hand.Play(typeof(CelaenoHarpy), out tasks, default, placePoint);
                }
                if (Hand.Contain(typeof(Cyclops)))
                {
                    tasks.Add(new TooltipTask("Katakan->Consume"));
                    return Hand.Play(typeof(Cyclops), out tasks, default, placePoint);
                }
            }

            #endregion

            #region Play rest of cards

            // hold on R1
            if (Extra.CurrentRound == Rounds.FirstRound &&
                Extra.MustPlayCard == false)
            {
                tasks = new List<IBotTask>
                {
                    new PassTask("No cards to play in first round")
                };
                return true;
            }

            // Dump from low to high
            foreach (var card in FromHighToLowStr.Reverse())
                if (Hand.Contain(card))
                    return Hand.Play(card, out tasks, leastBusyRow);

            // Maybe we missed something ? Better check twice
            foreach (var card in Hand.GetCards())
                if (card.GetType() != typeof(WoodlandSpirit))
                    return card.Play(out tasks);

            #endregion

            return false;
        }

        private bool PassLogic(ref ICollection<IBotTask> tasks)
        {
            #region Pass if not enough points

            if (Extra.CurrentRound == Rounds.FirstRound || Extra.CurrentRound == Rounds.WeLead)
                if (Hand.Count() <= 4)
                {
                    if (Extra.OurScore + GetHandValue() < Extra.EnemyScore)
                    {
                        tasks.Add(new PassTask($"we have {Extra.OurScore} pts + {GetHandValue()} in hand. Not enough to beat {Extra.EnemyScore}"));
                        return true;
                    }
                    if (Extra.OurScore >= Extra.EnemyScore)
                    {
                        tasks.Add(new PassTask("ahead and bled enemy a bit"));
                        return true;
                    }
                    if (Extra.EnemyScore - Extra.OurScore > 13)
                    {
                        tasks.Add(new PassTask("more than 13 points behind"));
                        return true;
                    }
                }

            #endregion

            #region Pass if enemy passed

            if (Extra.IsEnemyPassed == true &&
                Extra.OurScore > Extra.EnemyScore &&
                Extra.OurScore > 1)
            {
                tasks.Add(new PassTask("we are ahead and enemy passed"));
                return true;
            }

            #endregion

            #region Pass R1/R2 logic

            if (Extra.CurrentRound == Rounds.FirstRound &&
                Extra.OurScore > Extra.EnemyScore &&
                Hand.Count() <= 4)
            {
                tasks.Add(new PassTask("first round. lets pass for CA"));
                return true;
            }

            if (Extra.CurrentRound == Rounds.WeLead &&
                Extra.OurScore > Extra.EnemyScore &&
                Hand.Count() <= 3)
            {
                tasks.Add(new PassTask("bled a bit. lets pass"));
                return true;
            }

            if (Extra.CurrentRound == Rounds.WeLead &&
                Hand.Count() <= 7 &&
                Extra.OurScore == 0 &&
                Extra.EnemyScore == 0)
            {
                tasks.Add(new PassTask("lets pass for CA"));
                return true;
            }

            #endregion

            return false;
        }

        private bool SomethingToBuff()
        {
            return Board.GetCards().Any(unit => unit.GetType() != typeof(Werewolf) && unit.GetType() != typeof(AlphaWerewolf));
        }

        private bool HaveOtherImmuneShit()
        {
            return Hand.Contain(typeof(WoodlandSpirit)) && Hand.Contain(typeof(Werewolf));
        }

        #region Persistence data. Cleared at game end

        private bool LeaderBuffMode { get; set; }
        private bool GhoulMode { get; set; }
        private bool OzzrelMode { get; set; }
        private bool WeavessIncantationMode { get; set; }

        public void ResetAfterGame()
        {
            LeaderBuffMode = false;
            GhoulMode = false;
            OzzrelMode = false;
            WeavessIncantationMode = false;
        }

        #endregion

        public string Description => "Big monster deck with WOODLAND spirit leader";
    }
}
