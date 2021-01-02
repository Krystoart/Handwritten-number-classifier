using Microsoft.ML.Data;

public class MnistItem
{
    [ColumnName("Pixels")]
    [VectorType(784)]
    public float[] Pixels { get; } // 0 - 1 (white - black)
    public float Label { get; }
    public int Length { get; } // 784
    public MnistItem(int length, byte[] pixels)
    {
        this.Length = length;
        this.Label = -1;

        this.Pixels = new float[length];

        // Normalizes the pixel values from 0 - 1
        for (int i = 0; i < length; ++i)
            this.Pixels[i] = ((float)pixels[i]) / byte.MaxValue;
    }
}

class MnistOutPutData
{
    [ColumnName("Score")]
    public float[] Score;
}
