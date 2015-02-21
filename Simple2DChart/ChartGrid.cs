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
using System.Windows.Forms;
using Simple2DChart.Axes;

namespace Simple2DChart
{
	/// <summary>
	/// Summary description for Grid.
	/// </summary>
	public class ChartGrid : ChartBaseComponent
	{
        public ChartGrid(IAxis AxaX, IAxis AxaY)
		{
            this.AxaX = AxaX;
            this.AxaY = AxaY;
		}
        public IAxis AxaX { get; set; }
        public IAxis AxaY { get; set; }
		public virtual void Draw(Graphics g)
		{
            for (int i = 1; i < AxaX.NoOfLabels; i++)
            {
                int X = AxaX.GetPositionFromIndex(i);
                g.DrawLine(Pen, X, AxaY.Bounds.Top, X, AxaY.Bounds.Bottom);
            }
            for (int i = 1; i < AxaY.NoOfLabels; i++)
            {
                int Y = AxaY.GetPositionFromIndex(i);
                g.DrawLine(Pen, AxaX.Bounds.Left, Y, AxaX.Bounds.Right, Y);
            }
		}
	}
}
