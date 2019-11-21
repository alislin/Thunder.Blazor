/* Ceated by Ya Lin. 2019/11/21 14:50:41 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 日历组件基础类
    /// </summary>
    public class TCalendar:TComponent
    {
        /// <summary>
        /// 选择日期
        /// </summary>
        public DateTime? CheckedDate { get; set; }
        /// <summary>
        /// 日历日期
        /// </summary>
        protected List<DayView> Days { get; }
        /// <summary>
        /// 年列表
        /// </summary>
        protected List<int> Years { get;  }
        /// <summary>
        /// 月份列表
        /// </summary>
        protected List<int> Months { get;  }
        public int StartYear { get; private set; }
        public int EndYear { get; private set; }

        public int SelectedYear => ShowDate.Year;
        public int SelectedMonth => ShowDate.Month;

        private DateTime ShowDate = new DateTime();

        public TCalendar() : this(null) { }

        public TCalendar(DateTime? markedDate)
        {
            Days = new List<DayView>();
            Years = new List<int>();
            Months = new List<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
            SetYearRange(ShowDate.Year - 10, ShowDate.Year + 6);
            Check(markedDate);
        }

        /// <summary>
        /// 设置年范围
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void SetYearRange(int start,int end)
        {
            Years.Clear();
            StartYear = start;
            EndYear = end;
            for (int i = start; i < end+1; i++)
            {
                Years.Add(i);
            }
        }

        /// <summary>
        /// 设置显示日期
        /// </summary>
        /// <param name="date"></param>
        public void SetDate(DateTime date)
        {
            var day = Days.Find(x => x.Day.Date == date.Date);
            if (day!=null && day.Status!= DayStatuses.NotCurrentMonth)
            {
                return;
            }

            Days.Clear();
            var startMonth = new DateTime(date.Year, date.Month, 1);
            var startDay = startMonth.AddDays(-(int)startMonth.DayOfWeek);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);
            var endDay = endMonth.AddDays(6 - (int)endMonth.DayOfWeek);
            var currentMonth = startMonth.Month;
            for (DateTime i = startDay; i <= endDay; i=i.AddDays(1))
            {
                //是否选择月
                var status = i.Month == currentMonth ? DayStatuses.Normal : DayStatuses.NotCurrentMonth;
                //是否选择日期
                status |= (CheckedDate ?? DateTime.MinValue).Date == i.Date ? DayStatuses.Marked : 0;
                //是否周末
                status |= ((int)i.DayOfWeek == 0 || (int)i.DayOfWeek == 6) ? DayStatuses.WeekEnd : 0;
                //是否今天
                status |= i.Date == DateTime.Now.Date ? DayStatuses.Today : 0;
                Days.Add(new DayView { Day = i, Status = status });
            }
        }

        /// <summary>
        /// 选择日期
        /// </summary>
        /// <param name="date"></param>
        public void Check(DateTime? date)
        {
            var d = date ?? DateTime.MinValue;
            if (date != null)
            {
                SetDate(d);
            }
            else
            {
                SetDate(DateTime.Now);
            }
        }

        /// <summary>
        /// 设置选择年
        /// </summary>
        /// <param name="year"></param>
        public void SetYear(int year)
        {
            ShowDate = new DateTime(year, ShowDate.Month, 1);
            SetDate(ShowDate);
        }

        /// <summary>
        /// 设置选择月
        /// </summary>
        /// <param name="month"></param>
        public void SetMonth(int month)
        {
            ShowDate = new DateTime(ShowDate.Year, month, 1);
            SetDate(ShowDate);
        }

        /// <summary>
        /// 下一月
        /// </summary>
        public void NextMonth()
        {
            ShowDate = ShowDate.AddMonths(1);
            SetDate(ShowDate);
        }

        /// <summary>
        /// 上一月
        /// </summary>
        public void PrevMonth()
        {
            ShowDate = ShowDate.AddMonths(-1);
            SetDate(ShowDate);
        }


    }

    public class DayView
    {
        public DateTime Day { get; set; }
        public DayStatuses Status { get; set; }
        public int DayOfWeek => (int)Day.DayOfWeek;
    }

    [Flags]
    public enum DayStatuses
    {
        /// <summary>
        /// 普通
        /// </summary>
        Normal=0,
        /// <summary>
        /// 非当前月日期
        /// </summary>
        NotCurrentMonth=1,
        /// <summary>
        /// 标记
        /// </summary>
        Marked = 2,
        /// <summary>
        /// 今天
        /// </summary>
        Today=4,
        /// <summary>
        /// 周末
        /// </summary>
        WeekEnd=8,
        /// <summary>
        /// 假期
        /// </summary>
        Holoday=0x10,
        /// <summary>
        /// 禁用
        /// </summary>
        Disable=0x1000,
    }
}
