using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace gwent_daily_reborn.Model.Recognition
{
    internal interface IImageCompare
    {
        /// <summary>
        ///     SearchForPixel
        /// </summary>
        /// <param name="image"></param>
        /// <param name="color">color to search</param>
        /// <param name="tolerance">Euclidean distance (margin of error)</param>
        /// <param name="howManyHits">Minimal amount of hits to validate search</param>
        bool SearchForPixel(Image<Bgra, byte> image, Color color, int tolerance, int howManyHits);

        /// <summary>
        ///     Compares 2 images with same size
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <param name="threshould">minimum threshould from 0.0 to 1.0</param>
        bool AreSame(Image<Bgra, byte> image, Image<Bgra, byte> template, double threshould = 0.75);
    }
}
