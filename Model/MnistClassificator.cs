using System;
using Microsoft.ML;
using System.IO;
using MnistClassificator.DataStructures;

namespace MnistClassificator
{
    class Classificator
    {
        private static string BaseModelsRelativePath = @"../../../MLModels";
        private static string ModelRelativePath = $"{BaseModelsRelativePath}/Model.zip";
        private static string ModelPath = GetAbsolutePath(ModelRelativePath);

        private static MLContext mlContext = new MLContext();
        private static ITransformer trainedModel = mlContext.Model.Load(ModelPath, out var modelInputSchema);


    }

}