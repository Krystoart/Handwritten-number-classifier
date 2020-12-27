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
