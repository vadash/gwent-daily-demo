//using System;
//using System.Drawing;
//using System.IO;
//using System.Reflection;
//using System.Threading;
//using Emgu.CV;
//using Emgu.CV.OCR;
//using Emgu.CV.Structure;
//using gwent_daily_reborn.Model.Helpers.Tooltip;

//namespace gwent_daily_reborn.Model.Recognition
//{
//    internal class OpenCvOcrSt : IOcr
//    {
//        public OpenCvOcrSt()
//        {
//            var path = string.Empty;
//            try
//            {
//                if (path == string.Empty)
//                {
//                    var path1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
//                                "\\GwentBot\\app-" + Assembly.GetEntryAssembly().GetName().Version.ToString(3) +
//                                "\\tessdata";
//                    if (Directory.Exists(path1))
//                    {
//                        path = path1;
//                        Services.Container.GetInstance<ITooltip>().Show("D> OCR initialized from path # 1");
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                // ignored
//            }

//            try
//            {
//                if (path == string.Empty)
//                {
//                    var path2 = Directory.GetCurrentDirectory() + "\\tessdata";
//                    if (Directory.Exists(path2))
//                    {
//                        path = path2;
//                        Services.Container.GetInstance<ITooltip>().Show("D> OCR initialized from path # 2");
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                // ignored
//            }

//            try
//            {
//                if (path == string.Empty)
//                {
//                    var path3 = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents") +
//                                "\\tessdata";
//                    if (Directory.Exists(path3))
//                    {
//                        path = path3;
//                        Services.Container.GetInstance<ITooltip>().Show("D> OCR initialized from path # 3");
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                // ignored
//            }

//            try
//            {
//                if (path == string.Empty)
//                    throw new ApplicationException("Error with initializing OCR. Paths 1, 2 or 3 contain wrong info.");

//                OcrText = new Tesseract(path, "eng", OcrEngineMode.TesseractOnly);
//                OcrText.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
//                OcrText.SetVariable("tessedit_char_blacklist", ".,!?@#$%&*()<>_-+=/:;'\"");
//                OcrText.PageSegMode = PageSegMode.SingleLine;

//                OcrNumber = new Tesseract(path, "eng", OcrEngineMode.TesseractOnly);
//                OcrNumber.SetVariable("tessedit_char_whitelist", "0123456789");
//                OcrNumber.PageSegMode = PageSegMode.SingleWord;
//            }
//            catch (Exception ex)
//            {
//                //MessageBox.Show(ex.Message, @"Failed to initialize OCR");
//            }
//        }

//        private Tesseract OcrText { get; }
//        private Tesseract OcrNumber { get; }
//        private Image<Bgra, byte> ImageCv { get; set; }

//        private Mutex OcrMutex { get; } = new Mutex();

//        public bool SetImage(Image<Bgra, byte> image)
//        {
//            ImageCv = image;
//            return true;
//        }

//        /// <inheritdoc />
//        public bool AreSame(string expected, Rectangle roi)
//        {
//            var text = GetText(roi);
//            return AreClose(expected, text);
//        }

//        /// <inheritdoc />
//        public bool AreSame(string expected, Image<Gray, byte> image)
//        {
//            var text = GetText(image);
//            return AreClose(expected, text);
//        }

//        /// <inheritdoc />
//        public string GetText(Rectangle roi)
//        {
//            OcrMutex.WaitOne();
//            OcrText.SetImage(ImageCv.Copy(roi));
//            var result = OcrText.GetUTF8Text().ToUpper().Trim();
//            OcrMutex.ReleaseMutex();
//            return result;
//        }

//        /// <inheritdoc />
//        public int GetNumber(Rectangle roi)
//        {
//            OcrMutex.WaitOne();
//            OcrNumber.SetImage(ImageCv.Copy(roi));
//            var text = OcrNumber.GetUTF8Text().ToUpper().Trim();
//            int.TryParse(text, out var result);
//            OcrMutex.ReleaseMutex();
//            return result;
//        }

//        /// <inheritdoc />
//        public string GetText(Image<Gray, byte> image)
//        {
//            OcrMutex.WaitOne();
//            OcrText.SetImage(image);
//            var result = OcrText.GetUTF8Text().ToUpper().Trim();
//            OcrMutex.ReleaseMutex();
//            return result;
//        }

//        /// <summary>
//        ///     Compare 2 strings using Levenshtein distance. Alows up to 25% symbols be different
//        /// </summary>
//        /// <param name="expected"></param>
//        /// <param name="text"></param>
//        private static bool AreClose(string expected, string text)
//        {
//            expected = expected.ToUpper().Trim();
//            var variance = Math.Max(1, expected.Length / 4);
//            return Distance(text, expected) <= variance;
//        }

//        /// <summary>
//        ///     Compute the Levenshtein distance between two strings
//        /// </summary>
//        private static int Distance(string s, string t)
//        {
//            var n = s.Length;
//            var m = t.Length;
//            var d = new int[n + 1, m + 1];

//            // Step 1
//            if (n == 0)
//                return m;

//            if (m == 0)
//                return n;

//            // Step 2
//            for (var i = 0; i <= n; d[i, 0] = i++)
//            {
//            }

//            for (var j = 0; j <= m; d[0, j] = j++)
//            {
//            }

//            // Step 3
//            for (var i = 1; i <= n; i++)
//                //Step 4
//            for (var j = 1; j <= m; j++)
//            {
//                // Step 5
//                var cost = t[j - 1] == s[i - 1] ? 0 : 1;

//                // Step 6
//                d[i, j] = Math.Min(
//                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
//                    d[i - 1, j - 1] + cost);
//            }

//            // Step 7
//            return d[n, m];
//        }
//    }
//}


