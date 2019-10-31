using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestDataGeneration
{
    public static class PointExtensionFunctions
    {
        public static double SimilarityTo(this Point point1, Point point2, SimilarityCalculator.DistanceCalculationAlgorithm algorithm)
        {
            return algorithm(point1, point2);
        } 
    }
}
