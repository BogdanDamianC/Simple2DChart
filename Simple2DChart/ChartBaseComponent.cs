﻿/************************************
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Simple2DChart
{
    public enum Orientation { Horizontal, Vertical };
    public enum Position { Left, Right, Top, Bottom };
    public interface IChartComponent
    {
        Orientation Orientation { get; set; }
        Rectangle Bounds { get; set; }
        Font Font { get; set; }
        Brush Brush { get; set; }
        Pen Pen { get; set; }
    }

    public abstract class ChartBaseComponent : IChartComponent
    {
        public ChartBaseComponent()
        {
            this.Font = new Font(FontFamily.GenericSerif, 10);
            this.Brush = Brushes.Black;
            this.Pen = Pens.Black;
            this.Orientation = Orientation.Horizontal;
        }

        public Orientation Orientation { get; set; }
        public Rectangle Bounds { get; set; }
        public Font Font { get; set; }
        public Brush Brush { get; set; }
        public Pen Pen { get; set; }
    }
}
