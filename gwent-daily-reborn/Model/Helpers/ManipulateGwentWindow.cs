using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using gwent_daily_reborn.Model.Helpers.Tooltip;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Helpers
{
    internal static class Gwent
    {
        private static Process gwentProcess;

        public static Process GwentProcess
        {
            get
            {
                LazyLoadGwentInfo();
                return gwentProcess;
            }
        }

        private static void LazyLoadGwentInfo()
        {
            if (gwentProcess?.HasExited != false)
            {
                var processes = Process.GetProcessesByName("Gwent");
                gwentProcess = processes.Length > 0 ? processes[0] : null;
            }
        }

        public static bool ActivateGwent()
        {
            var hardware = Services.Container.GetInstance<IHardwareConstants>();
            var gwent = Process.GetProcessesByName("Gwent").FirstOrDefault()?.MainWindowHandle;
            if (gwent == null || gwent == IntPtr.Zero) return true;

            // Make it active
            if (gwent != GetForegroundWindow())
            {
                Services.Container.GetInstance<ITooltip>().Show("Moving focus to gwent window");
                SetForegroundWindow((IntPtr) gwent);
                Utility.SleepMedium();
            }

            // Delete gwent title
            var style = GetWindowLong((IntPtr) gwent, GwlStyle);
            if ((style & WsCaption) > 0)
            {
                Services.Container.GetInstance<ITooltip>().Show("Removing title bar from gwent window");
                SetWindowLong((IntPtr) gwent, GwlStyle, (int) (style & ~WsCaption));
                Utility.SleepMedium();
            }

            // Resize gwent
            GetWindowRect((IntPtr) gwent, out var rect);
            if (rect.Width != hardware.Width || rect.Height != hardware.Height)
            {
                Services.Container.GetInstance<ITooltip>().Show("Resizing gwent window");
                MoveWindow((IntPtr) gwent, 0, 0, hardware.Width, hardware.Height, false);
                Utility.SleepMedium();
            }

            return false; // we wanna continue
        }

        #region pinvoke 1

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            private readonly int Left;
            private readonly int Top;
            private readonly int Right;
            private readonly int Bottom;

            private Rect(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            private Rect(Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom)
            {
            }

            public int Height => Bottom - Top;

            public int Width => Right - Left;

            public static implicit operator Rectangle(Rect r)
            {
                return new Rectangle(r.Left, r.Top, r.Width, r.Height);
            }

            public static implicit operator Rect(Rectangle r)
            {
                return new Rect(r);
            }

            public static bool operator ==(Rect r1, Rect r2)
            {
                return r1.Equals(r2);
            }

            public static bool operator !=(Rect r1, Rect r2)
            {
                return !r1.Equals(r2);
            }

            private bool Equals(Rect r)
            {
                return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
            }

            public override bool Equals(object obj)
            {
                switch (obj)
                {
                    case Rect rect:
                        return Equals(rect);
                    case Rectangle rectangle:
                        return Equals(new Rect(rectangle));
                }

                return false;
            }

            public override int GetHashCode()
            {
                return ((Rectangle) this).GetHashCode();
            }

            public override string ToString()
            {
                return string.Format(CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top,
                    Right, Bottom);
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

        #endregion

        #region pinvoke 2

        /// Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is
        /// directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher
        /// priority to the thread that created the foreground window than it does to other threads.
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// Retrieves a handle to the foreground window (the window with which the user is currently working). The system
        /// assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads.
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private const int GwlStyle = -16;
        private const uint WsCaption = 0xC00000; //window with a title bar 

        #endregion
    }
}
