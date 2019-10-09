using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDataGeneration
{
    public partial class Form1 : Form
    {
        private DataSet[] dataSets = new DataSet[5]
        {
            new DataSet(),
            new DataSet(),
            new DataSet(),
            new DataSet(),
            new DataSet()
        };

        public Form1()
        {
            InitializeComponent();

            chart.Series["Series1"].Points.DataBind(dataSets[0].points, "X", "Y", null);
        }
    }
}
