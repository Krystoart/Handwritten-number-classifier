using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MnistClassificator.DataStructures
{
    class OutPutData
    {
        [ColumnName("Score")]
        public float[] Score;
    }
}