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


namespace Simple2DChart
{
	/// <summary>
	/// Summary description for TitluGrafic.
	/// </summary>


	public class ChartTitle
	{
		public string Text {get; set;}
        public Rectangle Bounds { get; set; }
        public Font Font { get; set; }
        public Brush Brush { get; set; }
        public Brush BackGroundBrush { get; set; }


        public ChartTitle(string Text, Rectangle Bounds)
		{
            this.Brush = Brushes.Black;
            this.Bounds = Bounds;
            this.Text = Text;
			this.Font = new Font(FontFamily.GenericSansSerif,30);
		}
		
		public ChartTitle(string Text, Rectangle Bounds,Font Font):this(Text, Bounds)
		{
            this.Font = Font;
		}

        public void Draw(Graphics g)
        {
            if (BackGroundBrush != null)
                g.FillRectangle(BackGroundBrush, Bounds);
            g.DrawString(Text, Font, Brush, Bounds);
        }
	}
}
