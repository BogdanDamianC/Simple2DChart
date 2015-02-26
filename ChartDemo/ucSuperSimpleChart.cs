using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simple2DChart;
using Simple2DChart.Axes;

namespace ChartDemo
{
    public partial class ucSuperSimpleChart : UserControl
    {
        Simple2DChart.ChartRenderer graphPrinter;

        public ucSuperSimpleChart()
        {
            InitializeComponent();
            CreateChart();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (graphPrinter != null && this.Visible)
                graphPrinter.Draw(e.Graphics);
            else
                base.OnPaint(e);
        }

        private void DrawPoint(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.Purple, x - 4, y - 4, 8, 8);
            g.FillPie(Brushes.Gold, x - 2, y - 2, 4, 4, 0, 180);
        }

        private void CreateChart()
        {
            var chartWidth = 800;
            var sampleTitle = new ChartTitle("Super Simple Chart", new Rectangle(100, 0, chartWidth, 50));
            sampleTitle.BackGroundBrush = Brushes.DarkSalmon;

            var axaX = new NumberAxis(new Rectangle(100, 400, chartWidth, 100), new Font(FontFamily.GenericSansSerif, 8), 5, Position.Bottom);
            axaX.LabelOrientation = Simple2DChart.Orientation.Vertical;
            axaX.Title = "Income";

            var axaY = new NumberAxis(new Rectangle(50, 100, 50, 300), new Font(FontFamily.GenericSansSerif, 8), 5, Position.Left);
            axaY.LabelOrientation = Simple2DChart.Orientation.Horizontal;
            axaY.Title = "Dollars $";

            var data = new Simple2DChart.Graphs.GraphData<double, double>[]{
                    new Simple2DChart.Graphs.GraphData<double, double>(1, 10),
                    new Simple2DChart.Graphs.GraphData<double, double>(2, 5),
                    new Simple2DChart.Graphs.GraphData<double, double>(3, 8),
                    new Simple2DChart.Graphs.GraphData<double, double>(4, 1),
                    new Simple2DChart.Graphs.GraphData<double, double>(5, 9)
            };
            axaX.MinValue = data.Min(d => d.X);
            axaX.MaxValue = data.Max(d => d.X);
            axaY.MinValue = data.Min(d => d.Y);
            axaY.MaxValue = data.Max(d => d.Y);


            var stepChart = new Simple2DChart.Graphs.LineGraph<double, double>(axaX, axaY, data);
            stepChart.Font = new Font(FontFamily.GenericSansSerif, 8);
            stepChart.DrawPoint += new Simple2DChart.Graphs.DrawPointDelegate(DrawPoint);
            stepChart.Pen = new Pen(Brushes.Maroon, 3);
            stepChart.Legend = "value tp 10";


            var grgrid = new ChartGrid(axaX, axaY);
            grgrid.Brush = new SolidBrush(System.Drawing.Color.CornflowerBlue);
            grgrid.Pen = new Pen(grgrid.Brush, Convert.ToSingle(0.2));


            graphPrinter = new ChartRenderer(new IAxis[] { axaY, axaX}, new Simple2DChart.Graphs.IGraph[] { stepChart}, sampleTitle);
            graphPrinter.Grid = grgrid;
        }

    }
}
