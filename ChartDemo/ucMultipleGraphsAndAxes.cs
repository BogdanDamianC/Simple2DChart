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
    public partial class ucMultipleGraphsAndAxes : UserControl
    {
        Simple2DChart.ChartRenderer graphPrinter;

        public ucMultipleGraphsAndAxes()
        {
            InitializeComponent();
            CreateChart();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (graphPrinter != null && this.Visible)
                graphPrinter.Draw(e.Graphics);
            //else
                base.OnPaint(e);
        }

        #region Draw Chart Points
        private void DrawPoint(Graphics g, int x, int y)
        {
            g.FillEllipse(Brushes.BlueViolet, x - 4, y - 4, 8, 8);
        }
        private void DrawPoint1(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.CadetBlue, x - 4, y - 4, 8, 8);
            g.FillPie(Brushes.Aquamarine, x - 2, y - 2, 4, 4, 0, 270);
        }
        private void DrawPoint2(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.Purple, x - 4, y - 4, 8, 8);
            g.FillPie(Brushes.Gold, x - 2, y - 2, 4, 4, 0, 180);
        }

        #endregion

        private void CreateChart()
        {
            var sampleTitle = new ChartTitle("Graph Demo with Multiple Graphs and Axes", new Rectangle(100, 0, 900, 100), new Font( FontFamily.Families[5], 35 ));
            sampleTitle.BackGroundBrush = Brushes.ForestGreen;
            sampleTitle.Brush = Brushes.LightCyan;

            var axaY = new NumberAxis(new Rectangle(900, 100, 75, 300), new Font(FontFamily.GenericMonospace, 8), 5, Position.Right);
            axaY.Label = "Euro";

            var axaX = new DateAxis(new Rectangle(100, 400, 800, 100), new Font(FontFamily.GenericSansSerif, 8), 10, Position.Bottom);
            axaX.Width = 15;
            axaX.Orientation = Simple2DChart.Orientation.Vertical;
            axaX.Label = "DateTime";

            var axaY2 = new NumberAxis(new Rectangle(50, 100, 50, 300), new Font(FontFamily.GenericSansSerif, 8), 5, Position.Left);
            axaY2.Label = "Dollars $";

            var data = new Simple2DChart.Graphs.GraphData<DateTime, double>[]{
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddDays(-3), 10),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddDays(-2), 5),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddDays(-1), 8),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now, 1),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddDays(2), 9)
            };
            axaX.MinValue = data.Min(d => d.X);
            axaX.MaxValue = data.Max(d => d.X);
            axaY.MinValue = data.Min(d => d.Y);
            axaY.MaxValue = data.Max(d => d.Y);

            axaY2.MinValue = 0;
            axaY2.MaxValue = 95;

            var data2 = new Simple2DChart.Graphs.GraphData<DateTime, double>[]{
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddDays(-3), 80),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddDays(-2), 5),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddDays(-1), 8),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now, 1),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddHours(25), 50),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddHours(27), 25),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddHours(36), 75),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddDays(2), 90)
            };

            var data3 = new Simple2DChart.Graphs.GraphData<DateTime, double>[]{
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddDays(-3), 70),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddDays(-2), 50),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddDays(-1), 80),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now, 10),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddHours(25), 5),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddHours(27), 75),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddHours(36), 25),
                    new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now.AddDays(2), 0)
            };

            var stepChart = new Simple2DChart.Graphs.StepLineGraph<DateTime, double>(axaX, axaY, data);
            stepChart.Font = new Font(FontFamily.GenericSansSerif, 8);
            stepChart.DrawPoint += new Simple2DChart.Graphs.DrawPointDelegate(DrawPoint);
            stepChart.Pen = Pens.BlueViolet;
            stepChart.Legend = "value tp 10";

            var lineChart = new Simple2DChart.Graphs.LineGraph<DateTime, double>(axaX, axaY2, data2);
            lineChart.Font = new Font(FontFamily.GenericSansSerif, 8);
            lineChart.DrawPoint += new Simple2DChart.Graphs.DrawPointDelegate(DrawPoint1);
            lineChart.Pen = Pens.Red;
            lineChart.Legend = "value to 100";

            var lineChartB = new Simple2DChart.Graphs.LineGraph<DateTime, double>(axaX, axaY2, data3, Simple2DChart.Graphs.LineGrapType.Curve);
            lineChartB.Font = new Font(FontFamily.GenericSansSerif, 8);
            lineChartB.DrawPoint += new Simple2DChart.Graphs.DrawPointDelegate(DrawPoint2);
            lineChartB.Pen = Pens.Green;
            lineChartB.Legend = "value to 100 Curve";


            var grgrid = new ChartGrid(axaX, axaY2);
            grgrid.Brush = new SolidBrush(System.Drawing.Color.CornflowerBlue);
            grgrid.Pen = new Pen(grgrid.Brush, Convert.ToSingle(0.2));


            graphPrinter = new ChartRenderer(new IAxis[] { axaY, axaX, axaY2 }, new Simple2DChart.Graphs.IGraph[] { stepChart, lineChart, lineChartB }, sampleTitle);
            graphPrinter.Grid = grgrid;
        }
    }
}
