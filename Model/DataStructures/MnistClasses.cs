using Microsoft.ML.Data;

public class MnistItem
{
    [ColumnName("Pixels")]
    [VectorType(784)]
    public float[] Pixels { get; } // 0 - 1 (white - black)
    public float Label { get; }
    public int Width { get; } // 28
    public int Height { get; } // 28
    public MnistItem(int width, int height, byte[][] pixels)
    {
        this.Width = width;
        this.Height = height;
        this.Label = -1;

        this.Pixels = new float[height*width];

        int counter = 0;
        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                this.Pixels[counter] = ((float)pixels[i][j]) / byte.MaxValue;
                counter += 1;
            }
        }
    }
}

class MnistOutPutData
{
    [ColumnName("Score")]
    public float[] Score;
}
