using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Alturos.Yolo;
using Alturos.Yolo.Model;

namespace gwent_daily_reborn.Model.NN
{
    internal class NeuralNet
    {
        private YoloWrapper YoloWrapper { get; }
        
        public NeuralNet(string alias)
        {
            var baseFolder = AppDomain.CurrentDomain.BaseDirectory;
            var cfgPath = $"{baseFolder}\\trainfiles\\{alias}.cfg";
            var weightsPath = $"{baseFolder}\\trainfiles\\{alias}.weights";
            var namesPath = $"{baseFolder}\\trainfiles\\{alias}.names";
            if (!File.Exists(cfgPath))
                throw new ApplicationException($"Cant load {cfgPath} Neural Net file");
            if (!File.Exists(weightsPath))
                throw new ApplicationException($"Cant load {weightsPath} Neural Net file");
            if (!File.Exists(namesPath))
                throw new ApplicationException($"Cant load {namesPath} Neural Net file");
            try
            {
                Console.WriteLine("Starting NN in FAST mode");
                YoloWrapper = new YoloWrapper(cfgPath, weightsPath, namesPath);
            }
            catch (Exception exception1)
            {
                Console.WriteLine(exception1);
                Console.WriteLine("Cant initialize GPU support. (Radeon ?) Switching to SLOW mode");
                try
                {
                    YoloWrapper = new YoloWrapper(cfgPath, weightsPath, namesPath, 0, true);
                }
                catch (Exception exception2)
                {
                    Console.WriteLine(exception2);
                    Console.WriteLine("Cant initialize SLOW mode. Aborting...");
                }
            }
            if (YoloWrapper == null)
                Console.WriteLine("Cant load YoloWrapper");
            if (!YoloWrapper.EnvironmentReport.CudaExists)
                Console.WriteLine("CUDA 10.0 not found. Switching to SLOW mode");
            if (!YoloWrapper.EnvironmentReport.CudnnExists)
                Console.WriteLine(@"x64\cudnn64_7.dll doesn't exist. Switching to SLOW mode");
            if (YoloWrapper.DetectionSystem.ToString() != "GPU")
                Console.WriteLine("No Nvidia GPU card detected. Switching to SLOW mode");
        }

        /// <summary>
        ///     Using neural net to recognise cards
        /// </summary>
        /// <param name="image"></param>
        /// <param name="confidence"></param>
        public IEnumerable<YoloItem> GetItems(Image image, double confidence = 0.2)
        {
            byte[] array;
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                array = ms.ToArray();
            }
            return YoloWrapper.Detect(array).Where(x => x.Confidence > confidence);
        }
    }
}
