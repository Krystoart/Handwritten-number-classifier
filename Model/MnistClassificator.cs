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
            var predEngine = mlContext.Model.CreatePredictionEngine<InputData, OutPutData>(trainedModel);

            var output = predEngine.Predict(input);

            return output.Score;
        }
    }

}