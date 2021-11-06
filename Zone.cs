using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ML2
{
    class Zone
    {
        (int x, int y) _centre;
        (int x, int y) _sigma;
        Color _color;
        string _name;

        public Zone(string name, (int, int) centre, (int, int) sigma, Color color)
        {
            _centre = centre;
            _sigma = sigma;
            _color = color;
            _name = name;
        }

        public Color Color { get => _color; }
        public (int x, int y) Sigma { get => _sigma; }
        public (int x, int y) Centre { get => _centre; }
        public string Name { get => _name; }
    }
}
