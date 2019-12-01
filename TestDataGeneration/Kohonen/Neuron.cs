using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestDataGeneration.Kohonen
{
    class Neuron
    {
        public Point coordinate;

        public Neuron(int line, int column)
        {
            coordinate = new Point(column, line);
        }
    }
}
