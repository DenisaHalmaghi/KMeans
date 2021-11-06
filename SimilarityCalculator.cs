using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ML2
{
    class SimilarityCalculator
    {
        public double calculate((int X, int Y, Color color) point, (int X, int Y, Color color) centroid)
        {
            var value = Math.Sqrt(Math.Pow(point.X - centroid.X, 2) + Math.Pow(point.Y - centroid.Y, 2));
            return value;
        }
    }
}
