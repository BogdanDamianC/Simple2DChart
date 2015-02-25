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
    public partial class ucSimpleProgressChart : UserControl
    {
        Simple2DChart.ChartRenderer graphRenderer;
        DateAxis axaX;
        NumberAxis axaY;

        Simple2DChart.Graphs.LineGraph<DateTime, double> instantNoOfRequestsChart;
        Simple2DChart.Graphs.LineGraph<DateTime, double> averageNoOfRequestsChart;


        public ucSimpleProgressChart()
        {
            InitializeComponent();
            CreateChart();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.DesignMode)
                return;
            timerRefresh.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            graphRenderer.Draw(e.Graphics);
            base.OnPaint(e);
        }



        #region Draw Chart Points
        private void DrawPoint(Graphics g, int x, int y)
        {
            g.FillEllipse(Brushes.BlueViolet, x - 2, y - 2, 4, 4);
        }
        private void DrawPoint1(Graphics g, int x, int y)
        {
            g.FillPie(Brushes.Aquamarine, x - 2, y - 2, 4, 4, 0, 270);
        }

        #endregion

        private void CreateChart()
        {
            var sampleTitle = new ChartTitle("Graph Demo with Multiple Graphs and Axes", new Rectangle(100, 0, 900, 20), new Font(FontFamily.GenericSerif, 15));
            sampleTitle.BackGroundBrush = Brushes.AliceBlue;
            sampleTitle.Brush = Brushes.LightCyan;


            axaX = new DateAxis(new Rectangle(50, 170, 800, 100), new Font(FontFamily.GenericSansSerif, 8), 10, Position.Bottom);
            axaX.Width = 15;
            axaX.Orientation = Simple2DChart.Orientation.Vertical;
            axaX.Label = null;
            axaX.GetLabel = (BaseAxis<DateTime> axis, int index, DateTime value) => {
                var seconds = Math.Floor( (value - axis.MinValue).TotalSeconds);
                var h = Math.Floor(seconds / 3600);
                seconds -= h * 3600;
                var m = Math.Floor(seconds / 60);
                seconds -= m * 60;
                string ret = string.Format("{0} seconds", seconds);
                if(m > 0)
                    ret = string.Format("{0} minutes - {1}", m, ret);
                if (h > 0)
                    ret = string.Format("{0} hours - {1}", h, ret);
                return ret;
            };

            axaY = new NumberAxis(new Rectangle(0, 20, 50, 150), new Font(FontFamily.GenericSansSerif, 8), 5, Position.Left);
            axaY.Label = "No of Requests/Second";


            instantNoOfRequestsChart = new Simple2DChart.Graphs.LineGraph<DateTime, double>(axaX, axaY, new List<Simple2DChart.Graphs.GraphData<DateTime, double>>());
            instantNoOfRequestsChart.Font = new Font(FontFamily.GenericSansSerif, 8);
            instantNoOfRequestsChart.DrawPoint += new Simple2DChart.Graphs.DrawPointDelegate(DrawPoint);
            instantNoOfRequestsChart.Pen = Pens.Red;
            instantNoOfRequestsChart.Legend = "Instant No Of Reuwqeusts/Second";

            averageNoOfRequestsChart = new Simple2DChart.Graphs.LineGraph<DateTime, double>(axaX, axaY, new List<Simple2DChart.Graphs.GraphData<DateTime, double>>(), Simple2DChart.Graphs.LineGrapType.Curve);
            averageNoOfRequestsChart.Font = new Font(FontFamily.GenericSansSerif, 8);
            averageNoOfRequestsChart.DrawPoint += new Simple2DChart.Graphs.DrawPointDelegate(DrawPoint1);
            averageNoOfRequestsChart.Pen = Pens.Green;
            averageNoOfRequestsChart.Legend = "Average No Of Reuwqeusts/Second";


            var grgrid = new ChartGrid(axaX, axaY);
            grgrid.Brush = new SolidBrush(System.Drawing.Color.CornflowerBlue);
            grgrid.Pen = new Pen(grgrid.Brush, Convert.ToSingle(0.2));

            axaX.MinValue = axaX.MaxValue = DateTime.Now;
            axaY.MinValue = 0;

            graphRenderer = new ChartRenderer(new IAxis[] { axaY, axaX, axaY }, new Simple2DChart.Graphs.IGraph[] { instantNoOfRequestsChart, averageNoOfRequestsChart }, sampleTitle);
            graphRenderer.Grid = grgrid;
        }

        Random randomValueGenerator = new Random();
        private void RefreshChartData()
        {
            double newInstant = randomValueGenerator.Next(500);
            double newAverage = randomValueGenerator.Next(500);
            ((List<Simple2DChart.Graphs.GraphData<DateTime, double>>)instantNoOfRequestsChart.GraphData).Add(new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now, newInstant));
            ((List<Simple2DChart.Graphs.GraphData<DateTime, double>>)averageNoOfRequestsChart.GraphData).Add(new Simple2DChart.Graphs.GraphData<DateTime, double>(DateTime.Now, newAverage));
            
            axaX.MaxValue = DateTime.Now;
            if(newInstant > axaY.MaxValue)
                axaY.MaxValue = newInstant;
            if (newAverage > axaY.MaxValue)
                axaY.MaxValue = newAverage;
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            RefreshChartData();
            //graphRenderer.Draw(this.CreateGraphics());
            this.Refresh();
        }

    }
}
