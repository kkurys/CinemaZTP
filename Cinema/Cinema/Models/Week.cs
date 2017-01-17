using System;
using System.Globalization;

namespace Cinema.Models
{
    public class Week
    {
        public Week(DateTime dt)
        {
            startTime = dt;
        }
        public DateTime startTime
        {
            get;
            set;
        }
        #region days of week
        public string FirstDay
        {
            get
            {
                var cultureInfo = new CultureInfo("pl-PL");
                var plDay = cultureInfo.DateTimeFormat.GetDayName(startTime.DayOfWeek);
                string s = plDay.ToString().Substring(0, 3) + ", " + startTime.Day + "." + startTime.Month;
                return s;
            }
        }
        public string SecondDay
        {
            get
            {
                var cultureInfo = new CultureInfo("pl-PL");
                var plDay = cultureInfo.DateTimeFormat.GetDayName(startTime.AddDays(1).DayOfWeek);
                string s = plDay.ToString().Substring(0, 3) + ", " + startTime.AddDays(1).Day + "." + startTime.AddDays(1).Month;
                return s;
            }
        }
        public string ThirdDay
        {
            get
            {
                var cultureInfo = new CultureInfo("pl-PL");
                var plDay = cultureInfo.DateTimeFormat.GetDayName(startTime.AddDays(2).DayOfWeek);
                string s = plDay.ToString().Substring(0, 3) + ", " + startTime.AddDays(2).Day + "." + startTime.AddDays(2).Month;
                return s;
            }
        }
        public string FourthDay
        {
            get
            {
                var cultureInfo = new CultureInfo("pl-PL");
                var plDay = cultureInfo.DateTimeFormat.GetDayName(startTime.AddDays(3).DayOfWeek);
                string s = plDay.ToString().Substring(0, 3) + ", " + startTime.AddDays(3).Day + "." + startTime.AddDays(3).Month;
                return s;
            }
        }
        public string FifthDay
        {
            get
            {
                var cultureInfo = new CultureInfo("pl-PL");
                var plDay = cultureInfo.DateTimeFormat.GetDayName(startTime.AddDays(4).DayOfWeek);
                string s = plDay.ToString().Substring(0, 3) + ", " + startTime.AddDays(4).Day + "." + startTime.AddDays(4).Month;
                return s;
            }
        }
        public string SixthDay
        {
            get
            {
                var cultureInfo = new CultureInfo("pl-PL");
                var plDay = cultureInfo.DateTimeFormat.GetDayName(startTime.AddDays(5).DayOfWeek);
                string s = plDay.ToString().Substring(0, 3) + ", " + startTime.AddDays(5).Day + "." + startTime.AddDays(5).Month;
                return s;
            }
        }
        public string SeventhDay
        {
            get
            {
                var cultureInfo = new CultureInfo("pl-PL");
                var plDay = cultureInfo.DateTimeFormat.GetDayName(startTime.AddDays(6).DayOfWeek);
                string s = plDay.ToString().Substring(0, 3) + ", " + startTime.AddDays(6).Day + "." + startTime.AddDays(6).Month;
                return s;
            }
        }
        #endregion

    }
}
