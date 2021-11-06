using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
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
            centroids = (new CentroidGenerator(5)).generate();
            builder = new MapBuilder(points, centroids);
        }

        private void moveCentroid()
        {
            var centroidToPointsMap = builder.build();
            var similarityCalculator = new SimilarityCalculator();
            (int x, int y) center;
            List<int> val;
            convergentaVeche = convergentaNoua;
            convergentaNoua = 0;
            foreach (var centroidIndex in centroidToPointsMap.Keys)
            {
                //? here
                centroidToPointsMap.TryGetValue(centroidIndex, out val);
                var pointsIndexes = centroidToPointsMap[centroidIndex];
                //if there are no points just leave the centroid there
                if (val.Count > 0)
                {
                    center = (0, 0);
                    foreach (var pointIndex in pointsIndexes)
                    {
                        var point = points[pointIndex];
                        //medie artiemtica pentru centrul de grutate
                        center.x += point.X;
                        center.y += point.Y;

                        //recalculam
                        convergentaNoua += similarityCalculator.calculate(point, centroids[centroidIndex]);
                    }

                    //avoid dividing by 0
                    var count = pointsIndexes.Count;
                    center.x /= count;
                    center.y /= count;

                    centroids[centroidIndex] = (center.x, center.y, centroids[centroidIndex].color);
                }



            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var drawer = new PointsDrawer(e);
            drawer.draw(points);
            drawer.draw(centroids, true);
            this.Text = "Initial";
            var epoca = 1;
            do
            {
                Thread.Sleep(2000);
                drawer.clean();
                if (epoca == 2)
                {

                }
                moveCentroid();
                drawer.draw(points);
                drawer.draw(centroids, true);
                this.Text = "Epoca " + epoca;
                epoca++;
            } while (convergentaNoua != convergentaVeche);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
