using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace gwent_daily_reborn.Model.Recognition
{
    internal class ImageCompare : IImageCompare
    {
        public bool AreSame(Image<Bgra, byte> image, Image<Bgra, byte> template, double threshould = 0.75)
        {
            if (image == null || template == null)
                return false;
            using (var result = image.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
            {
                result.MinMax(out _, out var maxValues, out _, out _);
                if (maxValues[0] > threshould)
                    return true;
            }
            return false;
        }

        public bool SearchForPixel(Image<Bgra, byte> image, Color color, int tolerance, int howManyHits)
        {
            if (image == null)
                return false;

            var toleranceSquared = tolerance * tolerance;
            var count = 0;
            var (r1, g1, b1) = (color.R, color.G, color.B);
            for (var x = 0; x < image.Width; x++)
            for (var y = 0; y < image.Height; y++)
            {
                int b2 = image.Data[y, x, 0];
                int g2 = image.Data[y, x, 1];
                int r2 = image.Data[y, x, 2];
                var diffR = r2 - r1;
                var diffG = g2 - g1;
                var diffB = b2 - b1;
                var distance = diffR * diffR + diffG * diffG + diffB * diffB;
                if (distance > toleranceSquared)
                    continue;
                count++;
                if (count >= howManyHits)
                    goto End;
            }
            End:
            return count >= howManyHits;
        }
    }
}
