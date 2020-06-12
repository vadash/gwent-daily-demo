using System;
using System.Collections.Generic;
using System.Threading;
using gwent_daily_reborn.Model.Control.ActionManager;
using gwent_daily_reborn.Model.Control.BotTasks;
using gwent_daily_reborn.Model.Control.Strategy;
using gwent_daily_reborn.Model.GameMemory;
using gwent_daily_reborn.Model.Helpers;
using gwent_daily_reborn.Model.Helpers.Keyboard;
using gwent_daily_reborn.Model.Recognition;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Control
{
    /// <summary>
    ///     This module handles general routines useful for all(most) decks
    ///     Ex. activates gwent, end turns, call IStrategy module
    /// </summary>
    internal class SimpleControl : IControl
    {
        private Mutex MainMutex { get; } = new Mutex();

        public void Do()
        {
            MainMutex.WaitOne();
            var info = Services.Container.GetInstance<IRecognition>().Update();
            var hardware = Services.Container.GetInstance<IHardwareConstants>();
            ICollection<IBotTask> tasks = new List<IBotTask>();
            switch (info)
            {
                #region Deck specific
                case Info.Mulligan:
                    Services.Container.GetInstance<IGameMemory>().NewRound();
                    Services.Container.GetInstance<IStrategy>().Play(out tasks, info);
                    break;
                case Info.ReadyToPlay:
                case Info.PickCard:
                case Info.EndTurn:
                case Info.MustPlayCard:
                    Services.Container.GetInstance<IStrategy>().Play(out tasks, info);
                    break;
                #endregion
                #region For every strategy
                case Info.DoNothing:
                    break;
                case Info.ActivateGwentWindow:
                    // todo proper way
                    break;
                case Info.GiveGG:
                    foreach (var obj in Services.Container.GetAllInstances<IResetAfterGame>())
                        obj.ResetAfterGame();
                    tasks = new List<IBotTask>
                        {
                            new GiveGg()
                        };
                    break;
                case Info.StartNewGame:
                    tasks = new List<IBotTask>
                        {
                            new StartGameTask()
                        };
                    break;
                case Info.CloseModalDialog1:
                    tasks = new List<IBotTask>
                        {
                            new TooltipTask("Closing window #1"),
                            new MouseMoveTask(hardware.ModalDialogButton1.Rectangle),
                            new LeftMouseClick(),
                            new SleepTask(3000)
                        };
                    break;
                case Info.CloseModalDialog2:
                    tasks = new List<IBotTask>
                    {
                        new TooltipTask("Closing window #2"),
                        new MouseMoveTask(hardware.ModalDialogButton2.Rectangle),
                        new LeftMouseClick(),
                        new SleepTask(3000)
                    };
                    break;
                case Info.CloseModalDialog3:
                    tasks = new List<IBotTask>
                    {
                        new TooltipTask("Closing window #3"),
                        new MouseMoveTask(hardware.ModalDialogButton3.Rectangle),
                        new LeftMouseClick(),
                        new SleepTask(3000)
                    };
                    break;
                case Info.CloseModalDialog4:
                    tasks = new List<IBotTask>
                    {
                        new TooltipTask("Closing window #4"),
                        new MouseMoveTask(hardware.ModalDialogButton4.Rectangle),
                        new LeftMouseClick(),
                        new SleepTask(3000),
                        new LeftMouseClick(),
                        new SleepTask(3000),
                    };
                    break;
                case Info.Stuck:
                    tasks = new List<IBotTask>
                        {
                            new TooltipTask("Stuck. Lets press Enter.."),
                            new KeyboardTask(Messaging.VKeys.Up),
                            new KeyboardTask(Messaging.VKeys.Left),
                            new KeyboardTask(Messaging.VKeys.Right),
                            new KeyboardTask(Messaging.VKeys.Down),
                            new KeyboardTask(Messaging.VKeys.Down),
                            new KeyboardTask(Messaging.VKeys.Enter)
                        };
                    break;
                case Info.EnemyTurn:
                    tasks = new List<IBotTask>
                        {
                            new TooltipTask("Enemy turn"),
                            new BmTask(),
                        };
                    break;
                #endregion
                default:
                    throw new NotImplementedException("Control");
            }
            Services.Container.GetInstance<IActionManager>().Do(tasks);
            MainMutex.ReleaseMutex();
        }
    }
}
