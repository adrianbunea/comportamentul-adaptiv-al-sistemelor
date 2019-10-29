using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestDataGeneration
{
    public partial class Form1 : Form
    {
        private DataSet[] dataSets;

        public Form1()
        {
            InitializeComponent();

            dataSets = new DataSet[3]
            {
                new DataSet(new Point(100,-250), new Point(40,40)),
                new DataSet(new Point(180,280), new Point(40,40)),
                new DataSet(new Point(-60,180), new Point(40,40))
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
                        foreach(DataSet dataSet in dataSets)
                        {
                            foreach(Point point in dataSet.points)
                            {
                                sw.WriteLine("{0},{1}", point.X, point.Y);
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
            dataSets = new DataSet[3]
            {
                new DataSet(new Point(0,0), new Point(40,40)),
                new DataSet(new Point(0,0), new Point(40,40)),
                new DataSet(new Point(0,0), new Point(40,40))
            };
            RedrawChart();
        }

        private void RedrawChart()
        {
            chart.Series.Clear();
            foreach (DataSet dataSet in dataSets)
            {
                Series series = new Series()
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
    }
}
