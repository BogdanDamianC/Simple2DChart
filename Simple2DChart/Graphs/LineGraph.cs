/************************************
Copyright 2015 Bogdan Damian
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

using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using Simple2DChart.Axes;


namespace Simple2DChart.Graphs
{
    public enum LineGrapType { SimpleLine, Curve};
    public class LineGraph<XType, YType> : BaseGraph<XType, YType>
    {
        public LineGraph(BaseAxis<XType> XAxis, BaseAxis<YType> YAxis, IEnumerable<GraphData<XType, YType>> GraphData, LineGrapType GraphType)
            : base(XAxis, YAxis, GraphData)
        {
            this.GraphType = GraphType;
        }

        public LineGraph(BaseAxis<XType> XAxis, BaseAxis<YType> YAxis, IEnumerable<GraphData<XType, YType>> GraphData)
            : this(XAxis, YAxis, GraphData, LineGrapType.SimpleLine)
        {
        }

        public LineGrapType GraphType { get; private set; }


        protected override void Draw2DGraph(Graphics g)
        {
            var points = (from gd in GraphData select new Point(XAxis.GetPosition(gd.X), YAxis.GetPosition(gd.Y))).ToArray();
            if (GraphType == LineGrapType.SimpleLine)
                g.DrawLines(Pen, points);
            else if (GraphType == LineGrapType.Curve)
                g.DrawCurve(Pen, points);
            if (this.DrawPoint != null)
                foreach(var currentPoint in points)
                    this.DrawPoint(g, currentPoint.X, currentPoint.Y);
        }
    }
}
