using System;

namespace VPL_course
{
    class Program
    {
        private static string pixelTrainingFile = @"./training-data/train-images-idx3-ubyte";
        private static string labelTrainingFile = @"./training-data/train-labels-idx1-ubyte";
        private static string pixelTestingFile = @"./training-data/t10k-images-idx3-ubyte";
        private static string labelTestingFile = @"./training-data/t10k-labels-idx1-ubyte";

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Loading data in memory...");
                MnistList trainingData = new MnistList(imagePath: pixelTrainingFile, labelPath: labelTrainingFile);
                Console.WriteLine("Data loading finished, Training amount: {0}, TestLength: {1}", trainingData.Length, 0);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
