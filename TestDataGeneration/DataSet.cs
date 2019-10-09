using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestDataGeneration
{
    class DataSet
    {
        public List<Point> points;

        public DataSet()
        {
            points = new List<Point>
            {
                new Point(45, 45),
                new Point(45, -45),
                new Point(-45, 45),
                new Point(-45, -45),
            };
        }
    }
}
