using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ML2
{
    class PointsDrawer
    {
        CartesianToScreenCoordinates converter = new CartesianToScreenCoordinates();
        PaintEventArgs canvas;

        public PointsDrawer(PaintEventArgs control)
        {
            this.canvas = control;
        }

        public void clean()
        {
            canvas.Graphics.Clear(Color.White);
        }

        public void draw(List<(int X, int Y, Color color)> points, bool large = false)
        {
            var pointSize = large ? 8 : 2;
            canvas.Graphics.DrawRectangle(new Pen(Color.Black), 0, 0, Constants.MAX * 2, Constants.MAX * 2);

            foreach (var point in points)
            {
                var brush = new SolidBrush(point.color);
                canvas.Graphics.FillRectangle(brush, converter.Ox(point.X), converter.Oy(point.Y), pointSize, pointSize);
            }
        }
    }
}