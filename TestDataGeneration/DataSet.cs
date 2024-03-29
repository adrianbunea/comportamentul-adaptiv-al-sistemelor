﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace TestDataGeneration
{
    class DataSet
    {
        static readonly Random random = new Random();
        private Point center;
        private Point dispersion;
        public List<Point> points;

        public DataSet(Point dispersion)
        {
            int x = random.Next(Constants.minX + 100, Constants.maxX - 100);
            int y = random.Next(Constants.minY + 100, Constants.maxY - 100);
            center = new Point(x, y);
            this.dispersion = dispersion;
            points = GenerateRandomPoints();
        }

        List<Point> GenerateRandomPoints()
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < 1000; i++)
            {
                points.Add(GenerateRandomPoint());
            }

            return points;
        }

        Point GenerateRandomPoint()
        {
            int x = GenerateX();
            int y = GenerateY();
            return new Point(x, y);
        }

        int GenerateX()
        {
            int x;
            while (true)
            {
                x = random.Next(Constants.minX, Constants.maxX);
                double gauss = Gauss(x, center.X, dispersion.X);
                double odds = random.Next(0, 1000) / 1000.0;
                if (odds < gauss) break;
            }
            
            return x;
        }

        int GenerateY()
        {
            int y;
            while (true)
            {
                y = random.Next(Constants.minY, Constants.maxY);
                double gauss = Gauss(y, center.Y, dispersion.Y);
                double odds = random.Next(0, 1000)/1000.0;
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
