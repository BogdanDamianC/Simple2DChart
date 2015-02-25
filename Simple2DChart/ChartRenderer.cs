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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;


namespace Simple2DChart
{
	/// <summary>
	/// Summary description for Printer.
	/// </summary>
	public class ChartRenderer
	{
        public Rectangle? LegendPosition { get; set; }
        public ChartGrid Grid { get; set; }
        public ChartTitle Title { get; set; }
        public IEnumerable<Simple2DChart.Axes.IAxis> Axes { get; set; }
        public IEnumerable<Graphs.IGraph> Graphs { get; set; }

        public ChartRenderer(IEnumerable<Simple2DChart.Axes.IAxis> Axes, IEnumerable<Graphs.IGraph> Graphs, ChartTitle Title)
		{
            this.Axes = Axes;
            this.Graphs = Graphs;
            this.Title = Title;
		}


        public ChartRenderer(IEnumerable<Simple2DChart.Axes.IAxis> Axes, Graphs.IGraph[] Graphs, ChartTitle Title, ChartGrid Grid, Rectangle? LegendPosition)
            : this(Axes, Graphs, Title)
		{
            this.Grid = Grid;
            this.LegendPosition = LegendPosition;
		}


        public Rectangle? GetBounds()
        {
            var bounds = new List<Rectangle>();
            if (Title != null)
                bounds.Add(Title.Bounds);
            if (Axes == null)
                bounds.AddRange(Axes.Select(a => a.Bounds));
            if (LegendPosition != null)
                bounds.Add(LegendPosition.Value);
            if (!bounds.Any())
                return null;

            int left = int.MaxValue, right = int.MinValue, top = int.MaxValue, bottom = int.MinValue;
            foreach (var b in bounds)
            {
                if (left > b.Left)
                    left = b.Left;
                if (right < b.Right)
                    right = b.Right;
                if (top > b.Top)
                    top = b.Top;
                if (bottom < b.Bottom)
                    bottom = b.Bottom;
            }
            return Rectangle.FromLTRB(left, top, right, bottom);
        }

        public void Draw(Graphics g)
        {
            if (Title != null)
                Title.Draw(g);
            if (Axes != null)
                foreach (var axe in Axes)
                    axe.PrepareForRendering();

            if (Grid != null)
                Grid.Draw(g);

            int noOfGraphs = Graphs != null ? Graphs.Count() : 0;
            if (noOfGraphs > 0)
            {
                int legendSlice = 0;

                if (LegendPosition.HasValue)
                    legendSlice = LegendPosition.Value.Height / noOfGraphs;

                int i = 0;
                foreach (var grph in Graphs)
                {
                    grph.Draw(g);
                    if (LegendPosition.HasValue)
                        grph.DrawLegend(g, new Rectangle(LegendPosition.Value.X, LegendPosition.Value.Y + i * legendSlice, LegendPosition.Value.Width, legendSlice));
                    i++;
                }
            }

            if (Axes != null)
                foreach (var axe in Axes)
                    axe.Draw(g);
        }

        public Bitmap GetImage()
        {
            var Bounds = GetBounds();
            if (!Bounds.HasValue)
                return new Bitmap(0,0);
            Bitmap bmp = new Bitmap(Bounds.Value.Width, Bounds.Value.Height);
            Graphics gg = Graphics.FromImage(bmp);
            gg.Clear(Color.White);
            Draw(gg);
            return bmp;
        }

        public void Save(string fileName)
        {
            using (Bitmap bmp = GetImage())
                bmp.Save(fileName);
        }

        public void Save(string fileName, System.Drawing.Imaging.ImageFormat format)
        {
            using (Bitmap bmp = GetImage())
                bmp.Save(fileName, format);
        }
	}
}
