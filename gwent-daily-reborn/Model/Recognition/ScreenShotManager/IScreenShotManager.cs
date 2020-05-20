using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace gwent_daily_reborn.Model.Recognition.ScreenShotManager
{
    internal interface IScreenShotManager
    {
        void UpdateImage();
        Image<Bgra, byte> CloneImage(Rectangle? section = null);
    }
}
