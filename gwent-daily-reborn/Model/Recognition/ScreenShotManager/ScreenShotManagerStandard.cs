using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using gwent_daily_reborn.Model.Settings;

namespace gwent_daily_reborn.Model.Recognition.ScreenShotManager
{
    /// <summary>
    /// Easy and simple but cant be used in .Net Core with CoreRT
    /// </summary>
    internal class ScreenShotManagerStandard : IScreenShotManager
    {
        private Image<Bgra, byte> Image { get; set; }

        public Image<Bgra, byte> CloneImage(Rectangle? section = null)
        {
            return section == null ? Image.Copy() : Image.Copy(section.Value);
        }

        public void UpdateImage()
        {
            var hardware = Services.Container.GetInstance<IHardwareConstants>();
            var selectedArea = new Rectangle(0, 0, hardware.Width, hardware.Height);
            var bitmap = new Bitmap(selectedArea.Width, selectedArea.Height);
            using var g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(Point.Empty, Point.Empty, selectedArea.Size);
            Image = new Image<Bgra, byte>(bitmap);
        }
    }
}



