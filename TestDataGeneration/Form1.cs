using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestDataGeneration
{
    public partial class Form1 : Form
    {
        private DataSet[] dataSets = new DataSet[3]
        {
            new DataSet(0, 50),
            new DataSet(240, 50),
            new DataSet(-290, 50)
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
                series.MarkerSize = 5;
                series.CustomProperties = "IsXAxisQuantitative=True";

                chart.Series.Add(series);
            }
        }
    }
}
