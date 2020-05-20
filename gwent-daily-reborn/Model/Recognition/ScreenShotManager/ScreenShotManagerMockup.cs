//using System.Drawing;
//using System.Drawing.Imaging;
//using Emgu.CV;
//using Emgu.CV.Structure;

//namespace gwent_daily_reborn.Model.Recognition.ScreenShotManager
//{
//    /// <summary>
//    /// For testing purpose
//    /// </summary>
//    internal class ScreenShotManagerMockup : IScreenShotManager
//    {
//        private ThreadSafeImage<Bgra, byte> ThreadSafeImage<Bgra, byte> { get; }

//        public ScreenShotManagerMockup(Image<Bgra, byte> image)
//        {
//            ThreadSafeImage<Bgra, byte> = new ThreadSafeImage<Bgra, byte>(image);
//        }

//        public Image<Bgra, byte> CloneImage(Rectangle? section = null)
//        {
//            return section == null ? ThreadSafeImage<Bgra, byte>.GetImage() : ThreadSafeImage<Bgra, byte>?.CloneImage(section.Value);
//        }

//        public void UpdateImage()
//        {
//        }

//        public Image<Bgra, byte> OpenCvImage()
//        {
//            var bitmap = CloneImage();
//            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
//            var result = new Image<Bgra, byte>(bitmap.Width, bitmap.Height, bitmapData.Stride, bitmapData.Scan0);
//            bitmap.UnlockBits(bitmapData);
//            return result;
//        }
//    }
//}



