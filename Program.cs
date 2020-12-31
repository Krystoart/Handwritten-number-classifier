using System;
using Microsoft.ML;
using System.IO;
using VPL_course.DataStructures;
using Microsoft.ML.Transforms;


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

                MLContext mlContext = new MLContext();
                Train(mlContext, trainingData, testingData);
                TestSomePredictions(mlContext);
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

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        private static void TestSomePredictions(MLContext mlContext)
        {
            ITransformer trainedModel = mlContext.Model.Load(ModelPath, out var modelInputSchema);

            // Create prediction engine related to the loaded trained model
            var predEngine = mlContext.Model.CreatePredictionEngine<MnistItem, MnistOutPutData>(trainedModel);

            var resultprediction1 = predEngine.Predict(SampleMNISTData.MNIST1);

            Console.WriteLine($"Actual: 1     Predicted probability:       zero:  {resultprediction1.Score[0]:0.####}");
            Console.WriteLine($"                                           One :  {resultprediction1.Score[1]:0.####}");
            Console.WriteLine($"                                           two:   {resultprediction1.Score[2]:0.####}");
            Console.WriteLine($"                                           three: {resultprediction1.Score[3]:0.####}");
            Console.WriteLine($"                                           four:  {resultprediction1.Score[4]:0.####}");
            Console.WriteLine($"                                           five:  {resultprediction1.Score[5]:0.####}");
            Console.WriteLine($"                                           six:   {resultprediction1.Score[6]:0.####}");
            Console.WriteLine($"                                           seven: {resultprediction1.Score[7]:0.####}");
            Console.WriteLine($"                                           eight: {resultprediction1.Score[8]:0.####}");
            Console.WriteLine($"                                           nine:  {resultprediction1.Score[9]:0.####}");
            Console.WriteLine();

            var resultprediction2 = predEngine.Predict(SampleMNISTData.MNIST2);

            Console.WriteLine($"Actual: 7     Predicted probability:       zero:  {resultprediction2.Score[0]:0.####}");
            Console.WriteLine($"                                           One :  {resultprediction2.Score[1]:0.####}");
            Console.WriteLine($"                                           two:   {resultprediction2.Score[2]:0.####}");
            Console.WriteLine($"                                           three: {resultprediction2.Score[3]:0.####}");
            Console.WriteLine($"                                           four:  {resultprediction2.Score[4]:0.####}");
            Console.WriteLine($"                                           five:  {resultprediction2.Score[5]:0.####}");
            Console.WriteLine($"                                           six:   {resultprediction2.Score[6]:0.####}");
            Console.WriteLine($"                                           seven: {resultprediction2.Score[7]:0.####}");
            Console.WriteLine($"                                           eight: {resultprediction2.Score[8]:0.####}");
            Console.WriteLine($"                                           nine:  {resultprediction2.Score[9]:0.####}");
            Console.WriteLine();

            var resultprediction3 = predEngine.Predict(SampleMNISTData.MNIST3);

            Console.WriteLine($"Actual: 9     Predicted probability:       zero:  {resultprediction3.Score[0]:0.####}");
            Console.WriteLine($"                                           One :  {resultprediction3.Score[1]:0.####}");
            Console.WriteLine($"                                           two:   {resultprediction3.Score[2]:0.####}");
            Console.WriteLine($"                                           three: {resultprediction3.Score[3]:0.####}");
            Console.WriteLine($"                                           four:  {resultprediction3.Score[4]:0.####}");
            Console.WriteLine($"                                           five:  {resultprediction3.Score[5]:0.####}");
            Console.WriteLine($"                                           six:   {resultprediction3.Score[6]:0.####}");
            Console.WriteLine($"                                           seven: {resultprediction3.Score[7]:0.####}");
            Console.WriteLine($"                                           eight: {resultprediction3.Score[8]:0.####}");
            Console.WriteLine($"                                           nine:  {resultprediction3.Score[9]:0.####}");
            Console.WriteLine();
        }
    }
}
