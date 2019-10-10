using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestDataGeneration
{
    public partial class Form1 : Form
    {
        private readonly DataSet[] dataSets = new DataSet[3]
        {
            new DataSet(new Point(100,-250), new Point(40,40)),
            new DataSet(new Point(180,280), new Point(40,40)),
            new DataSet(new Point(-60,180), new Point(40,40))
        };

        public Form1()
        {
            InitializeComponent();

            chart.Series.Clear();
            foreach(DataSet dataSet in dataSets)
            {
                Series series = new Series();
                series.Points.DataBind(dataSet.points, "X", "Y", null);
                series.ChartType = SeriesChartType.Point;
                series.BorderColor = Color.Transparent;
                series.MarkerSize = 3;
                series.CustomProperties = "IsXAxisQuantitative=True";

                chart.Series.Add(series);
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
        }
    }
}
