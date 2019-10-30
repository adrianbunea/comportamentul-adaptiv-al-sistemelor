using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;

namespace TestDataGeneration
{
    public partial class Form1 : Form
    {
        private List<DataSet> dataSets;
        private List<Point> points;

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

            RedrawChart();
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
            RedrawChart();
        }

        private void RedrawChart()
        {
            chart.Series.Clear();
            Series series = new Series()
            {
                ChartType = SeriesChartType.Point,
                BorderColor = Color.Transparent,
                MarkerSize = 3,
                CustomProperties = "IsXAxisQuantitative=True"
            };

            series.Points.DataBind(points, "X", "Y", null);
            chart.Series.Add(series);

            foreach (DataSet dataSet in dataSets)
            {
                series = new Series()
                {
                    ChartType = SeriesChartType.Point,
                    BorderColor = Color.Transparent,
                    MarkerSize = 3,
                    CustomProperties = "IsXAxisQuantitative=True"
                };

                series.Points.DataBind(dataSet.points, "X", "Y", null);
                chart.Series.Add(series);
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

                            dataSets.Clear();
                            chart.Series.Clear();
                        }
                        RedrawChart();
                        break;
                }

                sr.Close();
            }

            openFileDialog.Dispose();
        }
    }
}
