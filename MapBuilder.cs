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
            double maxSimilarity;
            int mostSimilarCentroid;

            var pointsCopy = points.ToList();
            foreach (var point in pointsCopy.Select((data, index) => (data, index)))
            {
                maxSimilarity = -1;
                mostSimilarCentroid = 0;
                foreach (var centroid in centroids.Select((data, index) => (data, index)))
                {
                    var calculatedSimilarity = similarityCalculator.calculate(point.data, centroid.data);

                    if (maxSimilarity < calculatedSimilarity)
                    {
                        maxSimilarity = calculatedSimilarity;
                        mostSimilarCentroid = centroid.index;
                    }
                }
                List<int> list;

                //change the color of the point to match the centroid it's being "assgined" to
                points[point.index] = (point.data.X, point.data.Y, centroids[mostSimilarCentroid].color);
                if (centroidToPointsMap.TryGetValue(mostSimilarCentroid, out list))
                {
                    list.Add(point.index);
                }
                else
                {
                    centroidToPointsMap.Add(mostSimilarCentroid, new List<int>(point.index));
                }

            }

            return centroidToPointsMap;
        }
    }
}
