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


    }

}