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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using Simple2DChart.Axes;


namespace Simple2DChart.Graphs
{
    public class StepLineGraph<XType, YType> : BaseGraph<XType, YType>
    {
        public StepLineGraph(BaseAxis<XType> XAxis, BaseAxis<YType> YAxis, IList<GraphData<XType, YType>> GraphData)
            : base(XAxis, YAxis, GraphData)
        {
        }

        protected override void Draw2DGraph(Graphics g)
        {
            GraphData<XType, YType> prevGD = null;
            foreach (var gd in GraphData)
            {
                int gdX = XAxis.GetPosition(gd.X);
                int gdY = YAxis.GetPosition(gd.Y);
                
                if (prevGD == null)
                {
                    prevGD = gd;
                    if (this.DrawPoint != null)
                        this.DrawPoint(g, gdX, gdY);
                    continue;
                }

                int prevGDX = XAxis.GetPosition(prevGD.X);
                int prevGDY = YAxis.GetPosition(prevGD.Y);
                g.DrawLine(Pen, prevGDX, prevGDY, gdX, prevGDY);
                g.DrawLine(Pen, gdX, prevGDY, gdX, gdY);

                if (this.DrawPoint != null)
                {
                    this.DrawPoint(g, gdX, prevGDY);
                    this.DrawPoint(g, gdX, gdY);
                }
                prevGD = gd;
            }
        }
    }
}
