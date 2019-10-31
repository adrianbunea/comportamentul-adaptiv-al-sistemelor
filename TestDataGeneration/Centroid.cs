using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestDataGeneration
{
    public class Centroid
    {
        private static Random random = new Random();
        public static List<Color> possibleColors = new List<Color>
        {
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue,
            Color.Purple,
            Color.Brown,
            Color.Magenta,
            Color.Cyan,
            Color.DarkOrchid,
        };

        private static int colorIndex = 0;

        public Point Coordinate { get; set; }
        public List<Point> points;
        public Color color;

        public Centroid()
        {
            points = new List<Point>();
            color = possibleColors[(colorIndex++) % 10];
            int x = random.Next(Constants.minX + 100, Constants.maxX - 100);
            int y = random.Next(Constants.minY + 100, Constants.maxY - 100);
            Coordinate = new Point(x, y);
        }

        public void RecalculateCoordinate()
        {
            int numberOfPoints = points.Count();
            if (numberOfPoints == 0) return;
            int xTotal = 0;
            int yTotal = 0;
            
            for (int i = 0; i < numberOfPoints; i++)
            {
                xTotal += points[i].X;
                yTotal += points[i].Y;
            }

            Coordinate = new Point(xTotal / numberOfPoints, yTotal / numberOfPoints);
        }
    }
}
