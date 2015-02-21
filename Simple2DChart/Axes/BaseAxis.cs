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
        string Label { get; set; }
        Position Position { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        void Draw(Graphics g);
        int GetPositionFromIndex(int i);
    }

    public abstract class BaseAxis<T> : ChartBaseComponent, IAxis
    {
        public T MaxValue { get; set; }
        public T MinValue { get; set; }

        public int NoOfLabels { get; set; }

        public string Label { get; set; }
        public Position Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public BaseAxis(Rectangle Bounds, Font Font, int NoOfLabels, Position Position)
        {
            this.Bounds = Bounds;
            this.Font = Font;
            this.NoOfLabels = NoOfLabels;
            this.Height = 15;
            this.Width = 15;
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
            DrawString(g, Label);

            StringFormat format = new StringFormat();
            if (Orientation == Orientation.Horizontal)
                format.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.DirectionRightToLeft;
            else
                format.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.DirectionVertical;
            if (this.Position == Position.Left || this.Position == Position.Bottom)
                format.Alignment = StringAlignment.Near;
            else
                format.Alignment = StringAlignment.Far;
            Func<int, Rectangle> getRect = null;
            if (this.Position == Position.Right || this.Position == Position.Left)
            {
                int width = Bounds.Width - 5;
                int offset = Height / 2;
                int left = Bounds.Left;
                if (this.Position == Position.Right)
                    left += 5;
                getRect = (int position)=> new Rectangle(Bounds.Left, position - offset, width, Height);
            }
            else
            {
                int heigth = Bounds.Height - 5;
                int offset = Width / 2;
                int top = Bounds.Top;
                if (this.Position == Position.Bottom)
                    top += 5;
                getRect = (int position)=> new Rectangle(position - offset, top, Width, heigth);
            }

            for (int i = 0; i <= NoOfLabels; i++)
            {
                var tmpValue = GetValueFromIndex(i);
                g.DrawString(GetLabel(i, tmpValue), Font, Brush, getRect(GetPosition(tmpValue)), format);
            }
        }

        private void DrawString(Graphics g, string str)
        {
            StringFormat format = new StringFormat();
            if (Orientation == Orientation.Horizontal)
                format.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.DirectionVertical;
            else
                format.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.DirectionRightToLeft;
            format.Alignment = StringAlignment.Center;
            if (this.Position == Position.Right)
                g.DrawString(str, Font, Brush,
                    new Rectangle(Bounds.Right - Height, Bounds.Top, Height, Bounds.Height), format);

            if (this.Position == Position.Left)
                g.DrawString(str, Font, Brush,
                    new Rectangle(Bounds.Left, Bounds.Top, Height, Bounds.Height), format);

            if (this.Position == Position.Bottom)
                g.DrawString(str, Font, Brush,
                    new Rectangle(Bounds.Left, Bounds.Bottom - Width, Bounds.Width, Width), format);
            if (this.Position == Position.Top)
                g.DrawString(str, Font, Brush,
                    new Rectangle(Bounds.Left, Bounds.Top, Bounds.Width, Width), format);
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
        public Func<int, T, string> GetLabel { get; set; }

    }
}
