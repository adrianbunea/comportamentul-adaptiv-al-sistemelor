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

        private Neuron[,] neurons;

        public Neurons()
        {
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
                    Neuron neuron = new Neuron(line * 60, column * 60);
                    SetNeuron(line + 5, column + 5, neuron);
                }
            }
        }
    }
}
