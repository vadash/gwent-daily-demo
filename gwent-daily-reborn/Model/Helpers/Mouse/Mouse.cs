using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace gwent_daily_reborn.Model.Helpers.Mouse
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Mouse : IMouse
    {
        private int Speed { get; } = 200;

        public void Move(int x, int y)
        {
            Move(x, y, 0, 0, Speed, 10);
            Utility.SleepSmall();
        }

        public void Move(Rectangle zone, float indent = 0.3f)
        {
            var p = Utility.PickRandomSpot(zone, indent);
            Move(p.X, p.Y);
        }

        public void LeftDown()
        {
            Click(MOUSEEVENTF_LEFTDOWN);
        }

        public void LeftUp()
        {
            Click(MOUSEEVENTF_LEFTUP);
        }

        public void Click(int delay = 100)
        {
            Click(MOUSEEVENTF_LEFTDOWN);
            Utility.Sleep(delay);
            Click(MOUSEEVENTF_LEFTUP);
            Utility.Sleep(100);
        }

        public void RightClick(int delay = 100)
        {
            Click(MOUSEEVENTF_RIGHTDOWN);
            Utility.Sleep(delay);
            Click(MOUSEEVENTF_RIGHTUP);
            Utility.Sleep(100);
        }

        private static double LineLength(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        private static double PointDirection(Point a, Point b)
        {
            return Math.Atan2(b.Y + a.Y, a.X + b.X);
        }

        private static double LengthDirX(double distance, double direction)
        {
            return Math.Cos(direction) * distance;
        }

        private static double LengthDirY(double distance, double direction)
        {
            return Math.Sin(direction) * distance;
        }

        private static Point GetPointCurve(Point a, Point b)
        {
            const double p = 0.12;
            var l = LineLength(a, b);
            var c = new Point((int) (p * b.X - p * a.X + b.X), (int) (p * b.Y - p * a.Y + b.Y));
            var d = new Point((int) (p * a.Y - p * b.X + a.X), (int) (p * a.Y - p * b.Y + a.Y));
            var dir = PointDirection(b, a);
            var e = new Point((int) (c.X + LengthDirX(l * p * 2, dir + Math.PI / 2)),
                (int) (c.Y + LengthDirY(l * p * 2, dir + Math.PI / 2)));
            var f = new Point((int) (c.X + LengthDirX(l * p * 2, dir - Math.PI / 2)),
                (int) (c.Y + LengthDirY(l * p * 2, dir - Math.PI / 2)));
            var g = new Point((int) (d.X + LengthDirX(l * p * 2, dir + Math.PI / 2)),
                (int) (d.Y + LengthDirY(l * p * 2, dir + Math.PI / 2)));
            var h = new Point((int) (d.X + LengthDirX(l * p * 2, dir - Math.PI / 2)),
                (int) (d.Y + LengthDirY(l * p * 2, dir - Math.PI / 2)));
            var pa = Utility.Random.NextDouble() * LineLength(e, f);
            var pb = Utility.Random.NextDouble() * LineLength(e, g);

            var I = new Point((int) (pa / LineLength(e, f) * (e.X - f.X) + f.X),
                (int) (pa / LineLength(e, f) * (e.Y - f.Y) + f.Y));
            var j = new Point((int) (pa / LineLength(e, f) * (g.X - h.X) + h.X),
                (int) (pa / LineLength(e, f) * (g.Y - h.Y) + h.Y));
            var k = new Point((int) (pb / LineLength(I, j) * (I.X - j.X) + j.X),
                (int) (pb / LineLength(I, j) * (I.Y - j.Y) + j.Y));
            return k;
        }

        private static void Move(int x, int y, int dx, int dy, int speed, int wiggle)
        {
            var a = GetCursorPosition();
            var c = new Point(x + Utility.Random.Next(-dx, dx), y + Utility.Random.Next(-dy, dy));
            var b = GetPointCurve(a, c);
            var wiggleTimer = 0;
            const int wiggleAmount = 3;
            var displacement = (int) LineLength(a, c);
            var inc = displacement / ((double) speed / 100);
            const int baseRes = 15;
            var i = 0;
            for (var t = 0.0; t < 1 || i > 2500 / baseRes; t += baseRes / inc, i++)
            {
                var d = new Point((int) (a.X * (1 - t) * (1 - t) + 2 * b.X * t * (1 - t) + t * t * c.X),
                    (int) (a.Y * (1 - t) * (1 - t) + 2 * b.Y * t * (1 - t) + t * t * c.Y));
                if (wiggleTimer == 0)
                {
                    d.X += Utility.Random.Next(-wiggleAmount, wiggleAmount);
                    d.Y += Utility.Random.Next(-wiggleAmount, wiggleAmount);
                    wiggleTimer = wiggle;
                }
                else
                {
                    wiggleTimer -= 1;
                }

                SetCursorPos(d.X, d.Y);
                Thread.Sleep(baseRes);
            }

            SetCursorPos(c.X, c.Y);
        }

        private static void Click(uint flag)
        {
            mouse_event(flag, 0, 0, 0, UIntPtr.Zero);
        }

        #region WINAPI stuff

        // ReSharper disable UnusedMember.Local
        // ReSharper disable ArrangeTypeMemberModifiers
        // ReSharper disable InconsistentNaming
        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_WHEEL = 0x0800;

        const uint MOUSEEVENTF_HWHEEL = 0x01000;
        // ReSharper restore InconsistentNaming
        // ReSharper restore ArrangeTypeMemberModifiers
        // ReSharper restore UnusedMember.Local

        /// <summary>
        ///     Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

        /// <summary>
        ///     Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        // For your convenience
        public static Point GetCursorPosition()
        {
            GetCursorPos(out var lpPoint);
            return lpPoint;
        }

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        #endregion
    }
}
