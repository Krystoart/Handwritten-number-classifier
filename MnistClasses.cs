using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class MnistList : IEnumerable<MnistItem>
{
    MnistItem[] images;

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

        if (magic1 != 2051)
            throw new Exception("Not a valid MNIST image data set");

        if (magic2 != 2049)
            throw new Exception("Not a valid MNIST label data set");

        int imgCount = ReverseBytes(brImages.ReadInt32());
        int labelCount = ReverseBytes(brLabels.ReadInt32());

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
            yield return item; // ^Test if this works with foreach
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }
}

public class MnistItem
{
    public byte[][] Pixels { get; } // 0 - 255 (white - black)
    public byte Label { get; } // 0 - 9
    public int Width { get; } // 28
    public int Height { get; } // 28
    public MnistItem(int width, int height, byte[][] pixels, byte label)
    {
        this.Width = width;
        this.Height = height;
        this.Label = label;

        this.Pixels = new byte[height][];
        for (int i = 0; i < this.Pixels.Length; ++i)
            this.Pixels[i] = new byte[width];

        for (int i = 0; i < height; ++i)
            for (int j = 0; j < width; ++j)
                this.Pixels[i][j] = pixels[i][j];
    }

    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < this.Height; ++i)
        {
            for (int j = 0; j < this.Width; ++j)
            {
                if (this.Pixels[i][j] == 0)
                    s += " "; // white
                else if (this.Pixels[i][j] == 255)
                    s += "0"; // black
                else
                    s += "."; // gray
            }
            s += "\n";
        }
        s += ("Label: ", this.Label.ToString());
        return s;
    }
}
    }

    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                if (this.pixels[i][j] == 0)
                    s += " "; // white
                else if (this.pixels[i][j] == 255)
                    s += "0"; // black
                else
                    s += "."; // gray
            }
            s += "\n";
        }
        s += ("Label: {0}", this.label.ToString());
        return s;
    }
}
