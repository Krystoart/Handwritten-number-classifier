using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace VPL_course.DataStructures
{
    class InputData
    {
        [ColumnName("PixelValues")]
        [VectorType(64)]
        public float[] PixelValues;

        [LoadColumn(64)]
        public float Number;
    }
}