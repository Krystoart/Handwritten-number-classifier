using System;
using Microsoft.ML;
using System.IO;

namespace MnistClassificator
{
    class Classificator
    {
        private static string BaseModelsRelativePath = @"../../../MLModels";
        private static string ModelRelativePath = $"{BaseModelsRelativePath}/Model.zip";
        private static string ModelPath = GetAbsolutePath(ModelRelativePath);

        private static MLContext mlContext = new MLContext();
        private static ITransformer trainedModel = mlContext.Model.Load(ModelPath, out var modelInputSchema);

        private static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Classificator).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        public static float[] Analyze(byte[][] image)
        {
            if (image.Length != 28)
                throw new Exception("Image length is not 28.");

            MnistItem imageObject = new MnistItem(width: 28, height: 28, pixels: image);

            var predEngine = mlContext.Model.CreatePredictionEngine<MnistItem, MnistOutPutData>(trainedModel);

            var output = predEngine.Predict(imageObject);

            return output.Score;
        }
    }

}