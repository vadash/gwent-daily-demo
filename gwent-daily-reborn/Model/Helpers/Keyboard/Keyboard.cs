using System;

namespace gwent_daily_reborn.Model.Helpers.Keyboard
{
    internal class Keyboard : IKeyboard
    {
        public void Press(Messaging.VKeys key)
        {
            var handle = Gwent.GwentProcess?.MainWindowHandle ?? IntPtr.Zero;
            if (handle != IntPtr.Zero)
                new Key(key).Press(handle, true);
        }
    }
}
