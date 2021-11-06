using System;
using System.Collections.Generic;
using System.Drawing;

namespace ML2
{
    class CentroidGenerator
    {
        int numberOfCentroids;

        Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.BlueViolet, Color.Brown };
        public CentroidGenerator(int numberOfCentroids)
        {
            this.numberOfCentroids = numberOfCentroids;
        }

        public List<(int x, int y, Color color)> generate()
        {
            Random r = new Random();
            var centroids = new List<(int x, int y, Color color)>();

            for (int i = 0; i < numberOfCentroids; i++)
            {
                var x = r.Next(Constants.MIN, Constants.MAX);
                var y = r.Next(Constants.MIN, Constants.MAX);
                var colorIndex = i % (colors.Length);
                centroids.Add((x, y, colors[colorIndex]));
            }

            return centroids;
        }
    }
}
