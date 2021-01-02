using System;
using Microsoft.ML;
using System.IO;

namespace MnistClassificator
{
    class Classificator
    {
        private string BaseModelsRelativePath = @"../../../Model/MLModels";
        private ITransformer trainedModel;

        public Classificator() {
            string ModelRelativePath = $"{BaseModelsRelativePath}/Model.zip";
            string ModelPath = GetAbsolutePath(ModelRelativePath);

            MLContext mlContext = new MLContext();
            this.trainedModel = mlContext.Model.Load(ModelPath, out var modelInputSchema);
        }

        private string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Classificator).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        /*
            Input parameters
            ----------------
            image: 28x28 2d array, each value is between 0 and 255 (white - black)
        */
        public float[] Analyze(byte[] image)
        {
            if (image.Length != 784)
                throw new Exception("Image length is not 784.");

            MnistItem imageObject = new MnistItem(length: image.Length, pixels: image);

            MLContext mlContext = new MLContext();
            var predEngine = mlContext.Model.CreatePredictionEngine<MnistItem, MnistOutPutData>(this.trainedModel);

            var output = predEngine.Predict(imageObject);

            return output.Score;
        }
    }
}
