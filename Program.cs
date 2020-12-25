using System;
using System.IO;

namespace VPL_course
{
    class Program
    {
        private static string pixelTrainingFile = @"./training-data/train-images-idx3-ubyte";
        private static string labelTrainingFile = @"./training-data/train-labels-idx1-ubyte";
        private static MnistImg[] MnistImages = null;

        static void Main(string[] args)
        {
            MnistImages = LoadData(pixelTrainingFile, labelTrainingFile, 2);
        }

        private static MnistImg[] LoadData(string pixelFilePath, string labelFilePath, int imgAmount)
        {
            try
            {
                Console.WriteLine("Reading data in memory\n");

                FileStream fsImages = new FileStream(pixelFilePath, FileMode.Open); // Images
                FileStream fsLabels = new FileStream(labelFilePath, FileMode.Open); // Labels

                BinaryReader brImages = new BinaryReader(fsImages);
                BinaryReader brLabels = new BinaryReader(fsLabels);

                MnistImg[] results = new MnistImg[imgAmount];
                byte[][] pixels = new byte[28][];
                for (int i = 0; i < pixels.Length; i++)
                    pixels[i] = new byte[28];

                // Discard first values
                int magic1 = brImages.ReadInt32();
                int imgCount = brImages.ReadInt32();
                int numRows = brImages.ReadInt32();
                int numCols = brImages.ReadInt32();

                int magic2 = brLabels.ReadInt32();
                int numLabels = brLabels.ReadInt32();

                for (int di = 0; di < imgAmount; ++di)
                {
                    for (int i = 0; i < pixels.Length; ++i)
                    {
                        for (int j = 0; j < pixels.Length; j++)
                        {
                            byte b = brImages.ReadByte();
                            pixels[i][j] = b;
                        }
                    }
                    byte label = brLabels.ReadByte();

                    MnistImg newImg = new MnistImg(width: 28, height: 28, pixels: pixels, label: label);
                    results[di] = newImg;
                    // Console.WriteLine(newImg.ToString());
                    // Console.ReadLine();
                }

                fsImages.Close();
                brImages.Close();
                fsLabels.Close();
                brLabels.Close();

                Console.WriteLine("Data read in memory\n");
                Console.ReadLine();

                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return null;
            }
        }
    }
}
