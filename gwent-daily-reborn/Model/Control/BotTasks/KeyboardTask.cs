using System;
using System.Collections.Generic;
using gwent_daily_reborn.Model.Helpers;
using gwent_daily_reborn.Model.Helpers.Keyboard;

namespace gwent_daily_reborn.Model.Control.BotTasks
{
    internal class KeyboardTask : IBotTask
    {
        public KeyboardTask(Messaging.VKeys key)
        {
            KeySequence = new List<Messaging.VKeys>
            {
                key
            };
        }

        private IEnumerable<Messaging.VKeys> KeySequence { get; }

        public bool Do()
        {
            var handle = Gwent.GwentProcess?.MainWindowHandle ?? IntPtr.Zero;
            foreach (var keyCode in KeySequence)
                new Key(keyCode).Press(handle, true);
            Utility.SleepSmall();
            return true;
        }
    }
}
