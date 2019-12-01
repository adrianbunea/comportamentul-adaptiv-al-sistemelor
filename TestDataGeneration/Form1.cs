using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using TestDataGeneration.Kohonen;

namespace TestDataGeneration
{
    public partial class Form1 : Form
    {
        private List<DataSet> dataSets;
        private List<Point> points;
        private List<Centroid> centroids;
        private Neurons neurons;

        public Form1()
        {
            InitializeComponent();

            points = new List<Point>();
            dataSets = new List<DataSet>
            {
                new DataSet(new Point(40,40)),
                new DataSet(new Point(40,40)),
                new DataSet(new Point(40,40))
            };

            foreach (DataSet dataSet in dataSets)
            {
                chart.Series.Add(CreateSeries(dataSet.points));
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "*(*.txt)|*.txt",
                Title = "Save points as a text file"
            };
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.OpenFile());

                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        Random random = new Random();
                        int totalPointCount = dataSets.Count*1000;
                        for (int i = 0; i < totalPointCount; i++)
                        {
                            int index = random.Next(0, dataSets.Count);
                            Point point = dataSets[index].points[0];
                            dataSets[index].points.RemoveAt(0);
                            sw.WriteLine("{0}: {1},{2}", index, point.X, point.Y);

                            if (dataSets[index].points.Count == 0)
                            {
                                dataSets.RemoveAt(index);
                            }
                        }
                        break;
                }

                sw.Close();
            }

            saveFileDialog.Dispose();
        }

        private void generateSetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            points = new List<Point>();
            dataSets = new List<DataSet>
            {
                new DataSet(new Point(40,40)),
                new DataSet(new Point(40,40)),
                new DataSet(new Point(40,40))
            };

            foreach (DataSet dataSet in dataSets)
            {
                chart.Series.Add(CreateSeries(dataSet.points));
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "*(*.txt)|*.txt",
                Title = "Save points as a text file"
            };
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                StreamReader sr = new StreamReader(openFileDialog.OpenFile());

                switch (openFileDialog.FilterIndex)
                {
                    case 1:
                        dataSets.Clear();
                        chart.Series.Clear();
                        points = new List<Point>();
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            char[] separators = { ' ', ':', ',' };
                            string[] splitLine = line.Split(separators);
                            //int dataSetIndex = Int32.Parse(splitLine[0]);
                            int x = Int32.Parse(splitLine[2]);
                            int y = Int32.Parse(splitLine[3]);
                            points.Add(new Point(x, y));
                        }
                        chart.Series.Add(CreateSeries(points));
                        break;
                }

                sr.Close();
            }

            openFileDialog.Dispose();
        }

        private void stepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(Centroid centroid in centroids)
            {
                centroid.points.Clear();
            }

            SimilarityCalculator.DistanceCalculationAlgorithm algorithm = SimilarityCalculator.EuclideanDistance;
            for (int i = 0; i < points.Count; i++)
            {
                double minimumSimilarity = Int32.MaxValue;
                int centroidIndex = -1;
                for (int j = 0; j < centroids.Count; j++)
                {
                    double similarity = points[i].SimilarityTo(centroids[j].Coordinate, algorithm);
                    if (similarity < minimumSimilarity)
                    {
                        minimumSimilarity = similarity;
                        centroidIndex = j;
                    }
                }

                centroids[centroidIndex].points.Add(points[i]);
            }

            chart.Series.Clear();

            foreach (Centroid centroid in centroids)
            {
                centroid.RecalculateCoordinate();
                Series series = CreateSeries(new List<Point> { centroid.Coordinate });
                series.MarkerSize = 30;
                series.Color = centroid.color;
                chart.Series.Add(series);

                series = CreateSeries(centroid.points);
                series.Color = Color.FromArgb(127, centroid.color);
                chart.Series.Add(series);
            }
        }

        private void generateCentroidsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            centroids = new List<Centroid>();
            int numberOfCentroids = random.Next(2, 11);
            for (int i = 0; i < numberOfCentroids; i++)
            {
                centroids.Add(new Centroid());
                Series series = CreateSeries(new List<Point> { centroids[i].Coordinate });
                series.Color = centroids[i].color;
                series.MarkerSize = 30;
                chart.Series.Add(series);
            }

        }

        private void generateNeuronsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            neurons = new Neurons();
            RedrawNeurons();
        }

        private void RedrawNeurons()
        {
            chart.Series.Clear();
            foreach (List<Point> line in neurons.Lines)
            {
                Series series = CreateLineSeries(line);
                chart.Series.Add(series);
                series = CreateSeries(line);
                series.MarkerSize = 10;
                series.Color = Color.Black;
                series.MarkerStyle = MarkerStyle.Square;
                chart.Series.Add(series);
            }

            foreach (List<Point> column in neurons.Columns)
            {
                Series series = CreateLineSeries(column);
                chart.Series.Add(series);
                series = CreateSeries(column);
                series.MarkerSize = 10;
                series.Color = Color.Black;
                series.MarkerStyle = MarkerStyle.Square;
                chart.Series.Add(series);
            }
        }

        private Series CreateSeries(List<Point> points)
        {
            Series series = new Series()
            {
                ChartType = SeriesChartType.Point,
                BorderColor = Color.Transparent,
                MarkerSize = 3,
                CustomProperties = "IsXAxisQuantitative=True"
            };

            series.Points.DataBind(points, "X", "Y", null);

            return series;
        }

        private  Series CreateLineSeries(List<Point> points)
        {
            Series series = new Series()
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Black,
                BorderColor = Color.Transparent,
                MarkerSize = 3,
                CustomProperties = "IsXAxisQuantitative=True"
            };

            series.Points.DataBind(points, "X", "Y", null);

            return series;
        }

        private void stepToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            neurons.SetNeuron(0, 0, new Neuron(-270, -270));
            RedrawNeurons();
        }
    }
}
