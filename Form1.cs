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
        int convergentaNoua;
        int convergentaVeche;
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
                    convergentaNoua += (int)similarityCalculator.calculate(point, centroids[centroidIndex]);
                }

                //avoid dividing by 0
                var count = pointsIndexes.Count > 0 ? pointsIndexes.Count : 1;
                center.x /= count;
                center.y /= count;

                centroids[centroidIndex] = (center.x, center.y, centroids[centroidIndex].color);
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
                Thread.Sleep(1000);
                drawer.clean();
                moveCentroid();
                drawer.draw(points);
                drawer.draw(centroids, true);
                this.Text = "Epoca " + epoca;
                epoca++;
            } while (convergentaNoua != convergentaVeche);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* do
             {
                 moveCentroid();
             } while (convergentaNoua != convergentaVeche);*/
        }
    }
}
