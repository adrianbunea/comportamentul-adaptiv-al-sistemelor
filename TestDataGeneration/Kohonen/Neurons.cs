using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestDataGeneration.Kohonen
{
    class Neurons
    {
        public List<List<Point>> Lines;
        public List<List<Point>> Columns;
        private int epoch;
        private double[] rataInvatare = new double[5] { 1, 0.7, 0.4, 0.2, 0.1 };

        private Neuron[,] neurons;

        public Neurons()
        {
            epoch = 0;
            Lines = new List<List<Point>>();
            for (int i = 0; i < 10; i++)
            {
                Lines.Add(new List<Point>());
            }

            Columns = new List<List<Point>>();
            for (int i = 0; i < 10; i++)
            {
                Columns.Add(new List<Point>());
            }

            CreateNeurons();
            Console.WriteLine();
        }

        public void SetNeuron(int line, int column, Neuron neuron)
        {
            neurons[line, column] = neuron;

            if (Lines[line].Count < 10)
            {
                Lines[line].Add(neuron.coordinate);
            }
            else
            {
                Lines[line][column] = neuron.coordinate;
            }

            if (Columns[column].Count < 10)
            {
                Columns[column].Add(neuron.coordinate);
            }
            else
            {
                Columns[column][line] = neuron.coordinate;
            }
        }

        private Neuron GetNeuron(int line, int column)
        {
            return neurons[line, column];
        }

        private void CreateNeurons()
        {
            neurons = new Neuron[10, 10];
            for (int line = -5; line < 5; line++)
            {
                for (int column = -5; column < 5; column++)
                {
                    Neuron neuron = new Neuron(line * 60 + 30, column * 60 + 30);
                    SetNeuron(line + 5, column + 5, neuron);
                }
            }
        }

        public void NextEpoch(List<Point> points)
        {
            SimilarityCalculator.DistanceCalculationAlgorithm algorithm = SimilarityCalculator.EuclideanDistance;

            foreach (Point point in points)
            {
                Point winnerNeuronCoordinate = new Point(0,0);
                double minimumSimilarity = int.MaxValue;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    { 
                        double similarity = point.SimilarityTo(neurons[i, j].coordinate, algorithm);
                        if (similarity < minimumSimilarity)
                        {
                            minimumSimilarity = similarity;
                            winnerNeuronCoordinate = new Point(i, j);
                        }
                    }
                }

                Neuron winnerNeuron = neurons[winnerNeuronCoordinate.X, winnerNeuronCoordinate.Y];
                int newX = (int)(winnerNeuron.coordinate.X + rataInvatare[epoch] * (point.X - winnerNeuron.coordinate.X));
                int newY = (int)(winnerNeuron.coordinate.Y + rataInvatare[epoch] * (point.Y - winnerNeuron.coordinate.Y));
                winnerNeuron.coordinate = new Point(newX, newY);
                SetNeuron(winnerNeuronCoordinate.X, winnerNeuronCoordinate.Y, winnerNeuron);
            }

            epoch++;
        }
    }
}
