using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
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
        int epoca = 1;
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

            convergentaVeche = convergentaNoua;
            convergentaNoua = 0;
            foreach (var centroidIndex in centroidToPointsMap.Keys)
            {
                var pointsIndexes = centroidToPointsMap[centroidIndex];
                //if there are no points just leave the centroid there
                if (pointsIndexes.Count > 0)
                {
                    center = (0, 0);
                    foreach (var pointIndex in pointsIndexes)
                    {
                        var point = points[pointIndex];
                        //calculate mean
                        center.x += point.X;
                        center.y += point.Y;

                        convergentaNoua += similarityCalculator.calculate(point, centroids[centroidIndex]);
                    }

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

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Initial";
        }

        private void Form1_Shown(object sender, EventArgs e)
        {


        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            do
            {
                moveCentroid();
                this.Refresh();
                await Task.Delay(4000);
                this.Text = "Epoca " + epoca;

                epoca++;
            } while (convergentaNoua != convergentaVeche);
        }

        private void Form1_wn(object sender, EventArgs e)
        {

        }
    }
}
