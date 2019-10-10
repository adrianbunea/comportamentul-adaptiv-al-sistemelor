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
        readonly Random random = new Random();
        readonly int minX = -400, minY = -400, maxX = 400, maxY = 400;
        private Point center;
        private Point dispersion;
        public List<Point> points;

        public DataSet(Point mean, Point dispersion)
        {
            center = mean;
            this.dispersion = dispersion;
            points = GenerateRandomPoints();
        }

        List<Point> GenerateRandomPoints()
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < 1000; i++)
            {
                int x = GenerateX();
                int y = GenerateY();
                points.Add(new Point(x, y));
            }

            return points;
        }

        int GenerateX()
        {
            int x;
            while (true)
            {
                x = random.Next(minX, maxX);
                double gauss = Gauss(x, center.X, dispersion.X);
                double odds = random.NextDouble();
                if (odds < gauss) break;
            }
            
            return x;
        }

        int GenerateY()
        {
            int y;
            while (true)
            {
                y = random.Next(minY, maxY);
                double gauss = Gauss(y, center.Y, dispersion.Y);
                double odds = random.NextDouble();
                if (odds < gauss) break;
            }

            return y;
        }

        double Gauss(int value, double mean, double dispersion)
        {
            double delta = mean - value;
            double variance = Math.Pow(dispersion, 2);
            double power = Math.Pow(delta, 2) / (2 * variance);
            return Math.Pow(Math.E, -power);
        }
    }
}
