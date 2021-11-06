using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ML2
{
    class PointsReader
    {
        protected string filePath = "";
        public PointsReader(string path)
        {
            string[] paths = { Environment.CurrentDirectory, @"..\..\..\", path };
            filePath = Path.GetFullPath(Path.Combine(paths));
        }
        public List<(int X, int Y, Color color)> readPoints()
        {
            var points = new List<(int X, int Y, Color color)>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                //read the line by line
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(" ");
                    var point = (Int32.Parse(parts[0]), Int32.Parse(parts[1]), Color.Black);
                    points.Add(point);
                }
            }

            return points;
        }
    }
}