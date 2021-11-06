using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ML2
{
    public partial class Form1 : Form
    {
        List<(int X, int Y, Color color)> points;
        List<(int X, int Y, Color color)> centroids;
        MapBuilder builder;
        double convergentaNoua;
        double convergentaVeche;
        public Form1()
        {
            InitializeComponent();
            points = (new PointsReader("puncte.txt")).readPoints();
            centroids = (new CentroidGenerator(4)).generate();
            builder = new MapBuilder(points, centroids);
        }

        private void moveCentroid()
        {
            var centroidToPointsMap = builder.build();
            var similarityCalculator = new SimilarityCalculator();
            (int x, int y) center;
            foreach (var centroidIndex in centroidToPointsMap.Keys)
            {
                var pointsIndexes = centroidToPointsMap.GetValueOrDefault(centroidIndex);
                center = (0, 0);
                //salvam valoarea veche
                convergentaVeche = convergentaNoua;
                //initializam
                convergentaNoua = 0;
                foreach (var pointIndex in pointsIndexes)
                {
                    var point = points[pointIndex];
                    //medie artiemtica pentru centrul de grutate
                    center.x += point.X;
                    center.y += point.Y;

                    //recalculam
                    convergentaNoua += similarityCalculator.calculate(point, centroids[centroidIndex]);
                }

                center.x /= pointsIndexes.Count;
                center.y /= pointsIndexes.Count;

                centroids[centroidIndex] = (center.x, center.y, centroids[centroidIndex].color);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            (new PointsDrawer(e)).draw(points);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            do
            {
                moveCentroid();
            } while (convergentaNoua != convergentaVeche);
        }
    }
}
