using System.Drawing;

namespace gwent_daily_reborn.Model.Helpers.Mouse
{
    internal interface IMouse
    {
        void LeftDown();
        void LeftUp();
        void Click(int delay = 100);
        void Move(int x1, int y1);
        void Move(Rectangle zone, float indent = 0.3f);
        void RightClick(int delay = 100);
    }
}
