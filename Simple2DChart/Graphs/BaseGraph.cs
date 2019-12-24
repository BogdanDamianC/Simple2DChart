/************************************
Copyright 2015+ Bogdan Damian
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
http://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
**************************************/

using System.Collections.Generic;
using System.Drawing;
using Simple2DChart.Axes;


namespace Simple2DChart.Graphs
{
    public delegate void DrawPointDelegate(Graphics g, int x, int y);
    public interface IGraph
    {
        void Draw(Graphics g);
        void DrawLegend(Graphics g, Rectangle rect);
    }

    public abstract class BaseGraph<XType, YType> : ChartBaseComponent, IGraph
    {
        public BaseGraph(BaseAxis<XType> XAxis, BaseAxis<YType> YAxis, IList<GraphData<XType, YType>> GraphData)
		{
            this.XAxis = XAxis;
            this.YAxis = YAxis;
            this.GraphData = GraphData;
		}

        public IList<GraphData<XType, YType>> GraphData { get; private set; }

        public string Legend { get; set; }

        public DrawPointDelegate DrawPoint { get; set; }
        public BaseAxis<XType> XAxis { get; set; }
        public BaseAxis<YType> YAxis { get; set; }

		public void Draw(Graphics g)
        {
            if (GraphData.Count <= 2)
                return;
            Draw2DGraph(g);
        }

        protected abstract void Draw2DGraph(Graphics g);
		
		public virtual void DrawLegend(Graphics g,Rectangle rect)
		{
            if (Legend == null)
                return;
			g.DrawLine(Pen,rect.Left,rect.Top + (rect.Bottom - rect.Top)/2,rect.Left + rect.Width *1/3,rect.Top + (rect.Bottom - rect.Top)/2);
			DrawPoint(g,rect.Left + (rect.Width)/6,rect.Top + (rect.Bottom - rect.Top)/2);
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Far;
			g.DrawString(Legend,Font,Brush,rect,format);
		}
    }
}
