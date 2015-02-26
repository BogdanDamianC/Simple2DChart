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

namespace Simple2DChart.Axes
{
    public interface IAxis : IChartComponent
    {
        void PrepareForRendering();
        int NoOfLabels { get; set; }
        string Title { get; set; }
        Orientation LabelOrientation { get; set; }
        Position Position { get; set; }
        void Draw(Graphics g);
        int GetPositionFromIndex(int i);
    }

    public abstract class BaseAxis<T> : ChartBaseComponent, IAxis
    {
        public T MaxValue { get; set; }
        public T MinValue { get; set; }

        public int NoOfLabels { get; set; }

        public string Title { get; set; }
        public Orientation LabelOrientation { get; set; }
        public Position Position { get; set; }
        public BaseAxis(Rectangle Bounds, Font Font, int NoOfLabels, Position Position)
        {
            this.LabelOrientation = Simple2DChart.Orientation.Vertical;
            this.Bounds = Bounds;
            this.Font = Font;
            this.NoOfLabels = NoOfLabels;
            this.Position = Position;
            this.Brush = Brushes.Black;
        }

        public virtual void Draw(Graphics g)
        {
            if (Position == Position.Right)
                g.DrawLine(Pen, Bounds.Left, Bounds.Top, Bounds.Left, Bounds.Bottom);
            if (Position == Position.Left)
                g.DrawLine(Pen, Bounds.Right, Bounds.Top, Bounds.Right, Bounds.Bottom);
            if (Position == Position.Bottom)
                g.DrawLine(Pen, Bounds.Left, Bounds.Top, Bounds.Right, Bounds.Top);
            if (Position == Position.Top)
                g.DrawLine(Pen, Bounds.Left, Bounds.Bottom, Bounds.Right, Bounds.Bottom);
            if(!string.IsNullOrEmpty(Title))
                DrawTitle(g, Title);

            
            var format = new StringFormat();
            format.FormatFlags = StringFormatFlags.NoClip;
            if (LabelOrientation == Orientation.Vertical)
                format.FormatFlags |= StringFormatFlags.DirectionVertical;

            int textHeight = Convert.ToInt32(g.MeasureString("10A", Font).Height);
            Func<int, Rectangle> getRect = null;
            int offset = 0, top = 0;
            switch (Position)
            {
                case Simple2DChart.Position.Top:
                    if (LabelOrientation == Orientation.Horizontal)
                    {
                        format.Alignment = StringAlignment.Center;
                        offset = Bounds.Width / (NoOfLabels * 2);
                        top = Bounds.Bottom - textHeight;
                    }
                    else
                    {
                        format.Alignment = StringAlignment.Far;
                        offset = textHeight / 2;
                        top = Bounds.Top - 2;
                    }
                    getRect = (int position) => new Rectangle(position - offset, top, offset*2, textHeight);
                    break;
                case Simple2DChart.Position.Bottom:
                    if (LabelOrientation == Orientation.Horizontal)
                    {
                        format.Alignment = StringAlignment.Center;
                        offset = Bounds.Width / (NoOfLabels * 2); 
                    }
                    else
                    {
                        format.Alignment = StringAlignment.Near;
                        offset = textHeight / 2;
                    }
                    getRect = (int position) => new Rectangle(position - offset, Bounds.Top + 2, offset * 2, Bounds.Height);
                    break;
                case Simple2DChart.Position.Left:
                    if (LabelOrientation == Orientation.Vertical)
                    {
                        offset = Bounds.Height / (NoOfLabels * 2) ;
                    }
                    else
                    {
                        format.Alignment = StringAlignment.Far;
                        offset = textHeight / 2;
                    }
                        
                    getRect = (int position) => new Rectangle(Bounds.Left, position - offset, Bounds.Width, textHeight);
                    break;
                case Simple2DChart.Position.Right:
                    if (LabelOrientation == Orientation.Vertical)
                    {
                        offset = Bounds.Height / (NoOfLabels * 2);
                    }
                    else
                    {
                        format.Alignment = StringAlignment.Near;
                        offset = textHeight / 2;
                    }
                    getRect = (int position) => new Rectangle(Bounds.Left, position - offset, Bounds.Width, textHeight);
                    break;
            }


            for (int i = 0; i <= NoOfLabels; i++)
            {
                var tmpValue = GetValueFromIndex(i);
                g.DrawString(GetLabel(this, i, tmpValue), Font, Brush, getRect(GetPosition(tmpValue)), format);
            }
        }

        private void DrawTitle(Graphics g, string str)
        {
            StringFormat format = new StringFormat();
            int textHeight = Convert.ToInt32(g.MeasureString(str, Font).Height);
            Rectangle rect = Bounds;
            format.FormatFlags = StringFormatFlags.NoClip;
            switch(Position)
            {
                case Simple2DChart.Position.Top:
                    rect = Bounds;
                    break;
               case Simple2DChart.Position.Bottom:
                    rect = Rectangle.FromLTRB(Bounds.Left, Bounds.Bottom - textHeight, Bounds.Right, Bounds.Bottom);
                    break;
                case Simple2DChart.Position.Left:
                    format.FormatFlags |= StringFormatFlags.DirectionVertical;
                    rect = Bounds;
                    break;
               case Simple2DChart.Position.Right:
                    format.FormatFlags |= StringFormatFlags.DirectionVertical;
                    rect = Rectangle.FromLTRB(Bounds.Right - textHeight, Bounds.Bottom, Bounds.Right, Bounds.Bottom);
                    break;
            }
            

            format.Alignment = StringAlignment.Center;
            g.DrawString(str, Font, Brush, rect, format);
        }

        public int GetPositionFromIndex(int i)
        {
            return GetPosition(GetValueFromIndex(i));
        }

        public abstract void PrepareForRendering();
        public abstract int GetPosition(T val);
        protected abstract T GetValueFromIndex(int index);

        /// <summary>
        /// function that generates the label for the chart, you can set it to something else to generate the Label
        /// </summary>
        public Func<BaseAxis<T>, int, T, string> GetLabel { get; set; }

    }
}
