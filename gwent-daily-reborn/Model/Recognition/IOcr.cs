using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace gwent_daily_reborn.Model.Recognition
{
    internal interface IOcr
    {
        bool SetImageGlobal(Image<Bgra, byte> image);

        /// <summary>
        ///     OCR text in area and compares it to expected (A-Z only, no numbers)
        /// </summary>
        bool AreSame(string expected, Rectangle roi);

        /// <summary>
        ///     OCR text in image and compares it to expected (A-Z only, no numbers)
        /// </summary>
        bool AreSame(string expected, Image<Gray, byte> image);

        /// <summary>
        ///     OCR text in area
        /// </summary>
        string GetText(Rectangle roi);

        /// <summary>
        ///     OCR text from image (usefull when we need to preprocess it)
        /// </summary>
        string GetText(Image<Gray, byte> image);

        /// <summary>
        ///     OCR number in area
        /// </summary>
        /// <returns>positive integer if success, -1 else</returns>
        int GetNumber(Rectangle roi);

        /// <summary>
        ///     OCR number in image
        /// </summary>
        /// <returns>positive integer if success, -1 else</returns>
        int GetNumber(Image<Gray, byte> image);
    }
}
