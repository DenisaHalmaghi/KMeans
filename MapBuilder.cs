using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ML2
{
    class MapBuilder
    {
        List<(int X, int Y, Color color)> points;
        List<(int X, int Y, Color color)> centroids;
        Dictionary<int, List<int>> centroidToPointsMap;
        public MapBuilder(List<(int X, int Y, Color color)> _points, List<(int X, int Y, Color color)> _centroids)
        {
            points = _points;
            centroids = _centroids;
        }

        public Dictionary<int, List<int>> build()
        {
            centroidToPointsMap = new Dictionary<int, List<int>>();
            var similarityCalculator = new SimilarityCalculator();
            double bestSimilarity;
            int mostSimilarCentroid;

            var pointsCopy = points.ToList();
            foreach (var point in pointsCopy.Select((data, index) => (data, index)))
            {
                bestSimilarity = Double.MaxValue;
                mostSimilarCentroid = 0;
                foreach (var centroid in centroids.Select((data, index) => (data, index)))
                {
                    var calculatedSimilarity = similarityCalculator.calculate(point.data, centroid.data);

                    if (calculatedSimilarity < bestSimilarity)
                    {
                        bestSimilarity = calculatedSimilarity;
                        mostSimilarCentroid = centroid.index;
                    }
                }
                List<int> list;

                //change the color of the point to match the centroid it's being "assgined" to
                points[point.index] = (points[point.index].X, points[point.index].Y, centroids[mostSimilarCentroid].color);
                if (centroidToPointsMap.TryGetValue(mostSimilarCentroid, out list))
                {
                    list.Add(point.index);
                }
                else
                {
                    list = new List<int>();
                    list.Add(point.index);
                    centroidToPointsMap.Add(mostSimilarCentroid, list);
                }

            }

            return centroidToPointsMap;
        }
    }
}
