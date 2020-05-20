// ReSharper disable once RedundantUsingDirective
using System;
using System.Reflection;
using System.Threading;
using gwent_daily_reborn.Model;
using gwent_daily_reborn.Model.Cards;
using gwent_daily_reborn.Model.Control;
using gwent_daily_reborn.Model.Control.ActionManager;
using gwent_daily_reborn.Model.Control.Strategy;
using gwent_daily_reborn.Model.GameInfo;
using gwent_daily_reborn.Model.GameMemory;
using gwent_daily_reborn.Model.Helpers;
using gwent_daily_reborn.Model.Helpers.Keyboard;
using gwent_daily_reborn.Model.Helpers.Mouse;
using gwent_daily_reborn.Model.Helpers.Tooltip;
using gwent_daily_reborn.Model.NN;
using gwent_daily_reborn.Model.Recognition;
using gwent_daily_reborn.Model.Recognition.ScreenShotManager;
using gwent_daily_reborn.Model.Settings;
using SimpleInjector;

namespace gwent_daily_reborn
{
    internal static class ControlBot
    {
        private static Random Random { get; } = new Random();
        
        public static void AssembleDi()
        {
            Services.Container = new Container();
            Services.Container.Options.DefaultLifestyle = Lifestyle.Singleton;

            Services.Container.Register<IDeckList, Monster2DeckList>();
            Services.Container.Register<ITooltip, SimpleTooltip>();
            Services.Container.Register<IMouse, Mouse>();
            Services.Container.Register<IHardwareConstants, Hardware1080P>();
            Services.Container.Register<IRecognition, Recognition>();
            Services.Container.Register<IOcr, OpenCvOcrMt>();
            Services.Container.Register<IImageCompare, ImageCompare>();
            Services.Container.Register<ICardFactory, CardFactory>();
            Services.Container.Register<IHand, Hand>();
            Services.Container.Register<IBoard, Board>();
            Services.Container.Register<IControl, SimpleControl>();
            Services.Container.Register<IActionManager, ActionManager>();
            Services.Container.Register<IMulligan, Mulligan>();
            Services.Container.Register<IKeyboard, Keyboard>();
            Services.Container.Register<IScreenShotManager, ScreenShotManagerStandard>();
            Services.Container.Register<IExtra, Extra>();
            Services.Container.Register<IGameMemory, GameMemory>();
            Services.Container.Register<IStrategy, MonsterStrategy>();
            Services.Container.Register(() => new NeuralNet("gwent_monster2"));
            var resetAfterGameAssemblies = new[]
            {
                typeof(IResetAfterGame).Assembly
            };
            Services.Container.Collection.Register<IResetAfterGame>(resetAfterGameAssemblies);
            Services.Container.Verify();
        }

        public static void RunBotThread()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = false;
                while (!BotSettings.TerminateAsap)
                {
                    if (BotSettings.IsRunning)
                    {
                        // TODO move to control
                        Gwent.ActivateGwent();
                        try
                        {
                            Services.Container.GetInstance<IControl>().Do();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(@"ERROR :" + e.Message);
                        }
                    }
                    RandomDelay();
                }
            }).Start();
        }

        private static void RandomDelay()
        {
            var next = 100 * Random.NextDouble(); // 100 = 100%
            if (next < 0.3)
            {
                Thread.Sleep(10000);
            }
            else if (next < 1)
            {
                Thread.Sleep(5000);
            }
            else if(next < 5)
            {
                Thread.Sleep(1000);
            }
            else
            {
                Thread.Sleep(500);
            }
        }

        public static void RunKeyThread()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = false;
                while (!BotSettings.TerminateAsap)
                {
                    var isTabKeyDown = Messaging.GetKeyState(new Key(Messaging.VKeys.Tab));
                    var isF9KeyDown = Messaging.GetKeyState(new Key(Messaging.VKeys.F9));
                    var isF10KeyDown = Messaging.GetKeyState(new Key(Messaging.VKeys.F10));
                    if (isTabKeyDown)
                    {
                        BotSettings.IsRunning = false;
                        Services.Container.GetInstance<ITooltip>().Show("Bot paused");
                    }
                    else if (isF9KeyDown)
                    {
                        BotSettings.IsRunning = true;
                        Services.Container.GetInstance<ITooltip>().Show("Bot started");
                    }
                    else if (isF10KeyDown)
                    {
                        BotSettings.IsRunning = false;
                        Services.Container.GetInstance<ITooltip>().Show("Bot paused");
                    }
                    Thread.Sleep(100);
                }
            }).Start();
        }
    }
}
