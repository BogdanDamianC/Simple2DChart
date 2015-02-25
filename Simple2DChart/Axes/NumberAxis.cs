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
    public class NumberAxis: BaseAxis<double>
	{
        public NumberAxis(Rectangle Bounds, Font Font, int NoOfLabels, Position Position)
            : base(Bounds, Font, NoOfLabels, Position)
		{
            this.GetLabel = DefaultGetLabel;
		}

        protected double sliceValue, dataIntervalSize;
        public override void PrepareForRendering()
        {
            if (MinValue < MaxValue)
            {
                dataIntervalSize = MaxValue - MinValue;
                sliceValue = dataIntervalSize / NoOfLabels;
            }
            else
            {
                dataIntervalSize = 1;
                sliceValue = 0;
            }
        }

        public override int GetPosition(double val)
		{
			if(val > MaxValue)
				val = MaxValue;
            if (this.Position == Position.Right || this.Position == Position.Left)
                return Convert.ToInt16((double)Bounds.Bottom - (double)Bounds.Height * (val - MinValue) / dataIntervalSize);
			else
                return Convert.ToInt16((double)Bounds.Left + (double)Bounds.Width * (val - MinValue) / dataIntervalSize);
		}

        protected override double GetValueFromIndex(int i)
        {
            return sliceValue * i + MinValue;
        }

        public readonly Func<BaseAxis<double>, int, double, string> DefaultGetLabel = (BaseAxis<double> axis, int index, double value) => { return Convert.ToString(Math.Floor(value)); };
	}
}
