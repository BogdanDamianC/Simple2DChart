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

using System;
using System.Drawing;

namespace Simple2DChart.Axes
{
    public class DateAxis : BaseAxis<DateTime>
    {
        public DateAxis(Rectangle Bounds, Font Font, int NoOfLabels, Position Position)
            : base(Bounds, Font, NoOfLabels, Position)
		{
            GetLabel = DefaultGetLabel;
		}

        protected long sliceValue, dataIntervalSize;
        public override void PrepareForRendering()
        {
            if (MinValue < MaxValue)
            {
                dataIntervalSize = (MaxValue - MinValue).Ticks;
                sliceValue = dataIntervalSize / NoOfLabels;
            }
            else
            {
                dataIntervalSize = 1;
                sliceValue = 0;
            }
        }

        public override int GetPosition(DateTime val)
        {
            TimeSpan timeDifference = val.Subtract(MinValue);
            if (Position == Position.Right || this.Position == Position.Left)
                return Convert.ToInt16((double)Bounds.Top - (double)Bounds.Height * ((double)timeDifference.Ticks / (double)dataIntervalSize));
            else
                return Convert.ToInt16((double)Bounds.Left + (double)Bounds.Width * ((double)timeDifference.Ticks / (double)dataIntervalSize));
        }

        protected override DateTime GetValueFromIndex(int i)
        {
            TimeSpan extraTimeSpan = new TimeSpan(sliceValue * i);
            return MinValue.Add(extraTimeSpan);
        }

        public static string DefaultGetLabel(BaseAxis<DateTime> axis, int index, DateTime value) { return value.ToShortDateString(); }
        public static string CurrentValueMinValueDiffLabel(BaseAxis<DateTime> axis, int index, DateTime value) { return TimeDiffToStringInHoursMinutesSeconds(axis.MinValue, value); }

        public static string TimeDiffToStringInHoursMinutesSeconds(DateTime startValue, DateTime endValue)
        {
            var seconds = Math.Floor((endValue - startValue).TotalSeconds);
            var h = Math.Floor(seconds / 3600);
            seconds -= h * 3600;
            var m = Math.Floor(seconds / 60);
            seconds -= m * 60;
            string ret = string.Format("{0} seconds", seconds);
            if (m > 0)
                ret = string.Format("{0} minutes - {1}", m, ret);
            if (h > 0)
                ret = string.Format("{0} hours - {1}", h, ret);
            return ret;
        }
    }
}
