using System;

namespace VPL_course
{
    class Program
    {
        private static string pixelTrainingFile = @"./training-data/train-images-idx3-ubyte";
        private static string labelTrainingFile = @"./training-data/train-labels-idx1-ubyte";
        private static string pixelTestingFile = @"./training-data/t10k-images-idx3-ubyte";
        private static string labelTestingFile = @"./training-data/t10k-labels-idx1-ubyte";

        private static string BaseModelsRelativePath = @"../../../MLModels";
        private static string ModelRelativePath = $"{BaseModelsRelativePath}/Model.zip";
        private static string ModelPath = GetAbsolutePath(ModelRelativePath);

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Loading data in memory...");
                MnistList trainingData = new MnistList(imagePath: pixelTrainingFile, labelPath: labelTrainingFile);
                MnistList testingData = new MnistList(imagePath: pixelTestingFile, labelPath: labelTestingFile);
                Console.WriteLine("Data loading finished, Training amount: {0}, TestLength: {1}", trainingData.Length, testingData.Length);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Train(MLContext mlContext, MnistList trainingData, MnistList testingData)
        {
            try
            {
                // 1: Data loaded into mlContext
                var trainData = mlContext.Data.LoadFromEnumerable<MnistItem>(trainingData.images);
                var testData = mlContext.Data.LoadFromEnumerable<MnistItem>(testingData.images);

                // 2: Context data process configuration with pipeline data transformations
                var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", "Label", keyOrdinality: ValueToKeyMappingEstimator.KeyOrdinality.ByValue).
                    Append(mlContext.Transforms.Concatenate("Features", nameof(MnistItem.Pixels)).AppendCacheCheckpoint(mlContext));

                // 3: Set the training algorithm, then create and config the modelBuilder
                var trainer = mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "Label", featureColumnName: "Features");
                var trainingPipeline = dataProcessPipeline.Append(trainer);

                // 4: Train the model fitting to the DataSet
                Console.WriteLine("=============== Training the model ===============");
                ITransformer trainedModel = trainingPipeline.Fit(trainData);

                Console.WriteLine("===== Evaluating Model's accuracy with Test data =====");
                var predictions = trainedModel.Transform(testData);
                var metrics = mlContext.MulticlassClassification.Evaluate(data: predictions, labelColumnName: "Label", scoreColumnName: "Score");

                Common.ConsoleHelper.PrintMultiClassClassificationMetrics(trainer.ToString(), metrics);

                // If there is already a trained model then this will override that model
                mlContext.Model.Save(trainedModel, trainData.Schema, ModelPath);

                Console.WriteLine("The model is saved to {0}", ModelPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
