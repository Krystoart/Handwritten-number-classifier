using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class MnistList : IEnumerable<MnistItem>
{
    public MnistItem[] images;

    public int Length { get; }
    public int Rows { get; }
    public int Columns { get; }

    public MnistList(string imagePath, string labelPath, bool normalize = false)
    {
        FileStream fsImages = new FileStream(imagePath, FileMode.Open); // Images
        FileStream fsLabels = new FileStream(labelPath, FileMode.Open); // Labels

        BinaryReader brImages = new BinaryReader(fsImages);
        BinaryReader brLabels = new BinaryReader(fsLabels);

        int magic1 = ReverseBytes(brImages.ReadInt32());
        int magic2 = ReverseBytes(brLabels.ReadInt32());

        // Tests if dataset magic numbers are correct
        if (magic1 != 2051)
            throw new Exception("Not a valid MNIST image data set");

        if (magic2 != 2049)
            throw new Exception("Not a valid MNIST label data set");

        int imgCount = ReverseBytes(brImages.ReadInt32());
        int labelCount = ReverseBytes(brLabels.ReadInt32());

        // Checks if for each image there is a label
        if (imgCount != labelCount)
            throw new Exception("Number of items of the two files is not the same");

        int imgRows = ReverseBytes(brImages.ReadInt32());
        int imgCols = ReverseBytes(brImages.ReadInt32());

        this.Length = imgCount;
        if (normalize) // TODO see if I even need this
        {
            this.Rows = 1;
            this.Columns = imgRows * imgCols;
        }
        else
        {
            this.Rows = imgRows;
            this.Columns = imgCols;
        }

        this.images = new MnistItem[this.Length];
        byte[][] item = new byte[Rows][];
        for (int i = 0; i < item.Length; i++)
            item[i] = new byte[Columns];

        for (int di = 0; di < this.Length; ++di)
        {
            for (int i = 0; i < item.Length; ++i)
            {
                for (int j = 0; j < item.Length; j++)
                {
                    byte b = brImages.ReadByte();
                    item[i][j] = b;
                }
            }
            byte label = brLabels.ReadByte();

            MnistItem newImg = new MnistItem(width: 28, height: 28, pixels: item, label: label);
            images[di] = newImg;
            // Console.WriteLine(newImg.ToString());
            // Console.ReadLine();
        }

        fsImages.Close();
        brImages.Close();
        fsLabels.Close();
        brLabels.Close();
    }

    public static int ReverseBytes(int v)
    {
        byte[] intBytes = BitConverter.GetBytes(v);
        Array.Reverse(intBytes);
        return BitConverter.ToInt32(intBytes);
    }

    public IEnumerator<MnistItem> GetEnumerator()
    {
        foreach (MnistItem item in images)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }
}

public class MnistItem
{
    [ColumnName("Pixels")]
    [VectorType(784)]
    public float[] Pixels { get; } // 0 - 1 (white - black)
    public float Label { get; } // 0 - 9
    public int Width { get; } // 28
    public int Height { get; } // 28
    public MnistItem(int width, int height, byte[][] pixels, byte label)
    {
        this.Width = width;
        this.Height = height;
        this.Label = label;

        this.Pixels = new float[height*width];

        int counter = 0;
        // Console.Write("new byte[][] {\n");
        for (int i = 0; i < height; ++i)
        {
            // Console.Write("new byte[] {");
            for (int j = 0; j < width; ++j)
            {
                // Console.Write(pixels[i][j]+", ");
                this.Pixels[counter] = ((float)pixels[i][j]) / byte.MaxValue;
                counter += 1;
            }
            // Console.Write("},\n");
        }
        // Console.Write("\n}");
    }

    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < this.Height*this.Width; ++i)
        {
            if (i % 28 == 0){
                s += "\n";
            }
            if (this.Pixels[i] == 0)
                s += " "; // white
            else if (this.Pixels[i] == 1)
                s += "0"; // black
            else
                s += "."; // gray
        }
        s += ("Label: ", this.Label.ToString());
        return s;
    }
}


