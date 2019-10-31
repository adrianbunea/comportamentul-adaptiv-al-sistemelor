using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestDataGeneration
{
    public static class SimilarityCalculator
    {
        public delegate double DistanceCalculationAlgorithm(Point point1, Point point2);

        public static double EuclideanDistance(Point point1, Point point2)
        {
            double xSquared = Math.Pow((point1.X - point2.X), 2);
            double ySquared = Math.Pow((point1.Y - point2.Y), 2);
            return Math.Sqrt(xSquared + ySquared);
        }
    }
}
