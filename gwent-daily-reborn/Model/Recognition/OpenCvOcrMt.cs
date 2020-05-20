using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using gwent_daily_reborn.Model.Helpers.Tooltip;

namespace gwent_daily_reborn.Model.Recognition
{
    /// <summary>
    ///     Fully reinterable as well
    /// </summary>
    internal class OpenCvOcrMt : IOcr
    {
        private const int MaxTextOcr = 3;
        private const int MaxNumberOcr = 3;

        public OpenCvOcrMt()
        {
            #region THEMIDA_CHECK

            if (Security.WeCracked2())
                return;            

            #endregion

            var path = string.Empty;
            try
            {
                if (path == string.Empty)
                {
                    var path2 = Directory.GetCurrentDirectory() + "\\tessdata";
                    if (Directory.Exists(path2))
                    {
                        path = path2;
                        Services.Container.GetInstance<ITooltip>().Show("D> OCR initialized from path # 2");
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                if (path == string.Empty)
                    throw new ApplicationException("Error with initializing OCR. Paths 1, 2 or 3 contain wrong info.");

                ImageLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
                OcrText = new List<OcrWithMutex>();
                for (var i = 0; i < MaxTextOcr; i++)
                {
                    // fast eng version
                    var ocr = new Tesseract(path, "eng", OcrEngineMode.TesseractLstmCombined);
                    ocr.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
                    ocr.PageSegMode = PageSegMode.SingleLine;
                    OcrText.Add(new OcrWithMutex(ocr, new object()));
                }

                OcrNumber = new List<OcrWithMutex>();
                for (var i = 0; i < MaxNumberOcr; i++)
                {
                    // trained for digits using gwent font
                    var ocr = new Tesseract(path, "digits", OcrEngineMode.TesseractOnly);
                    ocr.SetVariable("tessedit_char_whitelist", "0123456789");
                    ocr.PageSegMode = PageSegMode.SingleWord;
                    OcrNumber.Add(new OcrWithMutex(ocr, new object()));
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message + " Failed to initialize OCR");
            }
        }

        private Semaphore TextSemaphore { get; } = new Semaphore(MaxTextOcr, MaxTextOcr);
        private ICollection<OcrWithMutex> OcrText { get; }
        private Semaphore NumberSemaphore { get; } = new Semaphore(MaxNumberOcr, MaxNumberOcr);
        private ICollection<OcrWithMutex> OcrNumber { get; }
        private ReaderWriterLockSlim ImageLock { get; }
        private Image<Bgra, byte> ImageCv { get; set; }

        private struct OcrWithMutex
        {
            public OcrWithMutex(Tesseract ocr, object locker)
            {
                Ocr = ocr;
                Locker = locker;
            }

            public Tesseract Ocr { get; }
            public object Locker { get; }
        }

        #region SetImage

        public bool SetImageGlobal(Image<Bgra, byte> image)
        {
            var success = true;
            ImageLock.EnterWriteLock();
            try
            {
                ImageCv = image;
            }
            catch
            {
                success = false;
            }
            finally
            {
                ImageLock.ExitWriteLock();
            }
            return success;
        }

        private bool SetImageLocal(Tesseract ocr, IInputArray image)
        {
            var success = true;
            ImageLock.EnterReadLock();
            try
            {
                ocr.SetImage(image);
            }
            catch
            {
                success = false;
            }
            finally
            {
                ImageLock.ExitReadLock();
            }
            return success;
        }

        #endregion

        #region Text

        /// <inheritdoc />
        public bool AreSame(string expected, Rectangle roi)
        {
            var text = GetText(roi);
            return AreClose(expected, text);
        }

        public bool AreSame(string expected, Image<Gray, byte> image)
        {
            var text = GetText(image);
            return AreClose(expected, text);
        }

        public string GetText(Image<Gray, byte> image)
        {
            return GetText(image, null);
        }

        /// <inheritdoc />
        public string GetText(Rectangle roi)
        {
            return GetText(null, roi);
        }

        private string GetText(Image<Gray, byte> image, Rectangle? roi)
        {
            var result = "";
            TextSemaphore.WaitOne();
            foreach (var ocrMutex in OcrText)
            {
                var lockTaken = false;
                try
                {
                    Monitor.TryEnter(ocrMutex.Locker, 0, ref lockTaken);
                    if (lockTaken)
                    {
                        if (image != null)
                            SetImageLocal(ocrMutex.Ocr, image);
                        else if (roi != null)
                            SetImageLocal(ocrMutex.Ocr, ImageCv.Copy(roi.Value));
                        else
                            throw new ApplicationException("Error in GetText(Image<Gray, byte> image, Rectangle? roi)");
                        result = ocrMutex.Ocr.GetUTF8Text().ToUpper().Trim();
                    }
                }
                finally
                {
                    if (lockTaken)
                        Monitor.Exit(ocrMutex.Locker);
                }
                if (lockTaken)
                    break;
            }
            TextSemaphore.Release();
            return result;
        }

        /// <summary>
        ///     Compare 2 strings using Levenshtein distance. Alows up to 25% symbols be different
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="text"></param>
        private static bool AreClose(string expected, string text)
        {
            expected = expected.ToUpper().Trim();
            var variance = Math.Max(2, expected.Length / 4);
            return Distance(text, expected) <= variance;
        }

        /// <summary>
        ///     Compute the Levenshtein distance between two strings
        /// </summary>
        private static int Distance(string s, string t)
        {
            var n = s.Length;
            var m = t.Length;
            var d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
                return m;

            if (m == 0)
                return n;

            // Step 2
            for (var i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (var j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (var i = 1; i <= n; i++)
                //Step 4
            for (var j = 1; j <= m; j++)
            {
                // Step 5
                var cost = t[j - 1] == s[i - 1] ? 0 : 1;

                // Step 6
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }

            // Step 7
            return d[n, m];
        }

        #endregion

        #region Digits

        /// <inheritdoc />
        private int GetNumber(Image<Gray, byte> image, Rectangle? roi)
        {
            var result = -1;
            NumberSemaphore.WaitOne();
            foreach (var ocrMutex in OcrNumber)
            {
                var lockTaken = false;
                try
                {
                    Monitor.TryEnter(ocrMutex.Locker, 0, ref lockTaken);
                    if (lockTaken)
                    {
                        if (image != null)
                            SetImageLocal(ocrMutex.Ocr, image);
                        else if (roi != null)
                            SetImageLocal(ocrMutex.Ocr, ImageCv.Copy(roi.Value));
                        else
                            throw new ApplicationException("Error in GetNumber(Image<Gray, byte> image, Rectangle? roi)");
                        var text = ocrMutex.Ocr.GetUTF8Text().ToUpper().Trim();
                        text = MapSymbolsToDigits(text);
                        int.TryParse(text, out result);
                    }
                }
                finally
                {
                    if (lockTaken)
                        Monitor.Exit(ocrMutex.Locker);
                }
                if (lockTaken)
                    break;
            }
            NumberSemaphore.Release();
            return result;
        }

        /// <summary>
        ///     Use this method to improve digits OCR like replacing l with 1. Ideally it should be empty
        /// </summary>
        /// <param name="text"></param>
        private string MapSymbolsToDigits(string text)
        {
            return text;
        }

        public int GetNumber(Image<Gray, byte> image)
        {
            return GetNumber(image, null);
        }

        public int GetNumber(Rectangle roi)
        {
            return GetNumber(null, roi);
        }

        #endregion
    }
}
