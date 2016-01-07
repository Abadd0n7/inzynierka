using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inzynierka.ViewModel.Calendar
{
    public class CalendarListViewModel
    {
        public bool HasTodayEvents { get; set; }

        public IEnumerable<CalendarItem> TodayItems { get; set; }

        public bool HasPastEvents { get; set;
        }

        public IEnumerable<CalendarItem> PastItems
        {
            get; set;
        }

        public bool HasTomorrowItems { get; set; }

        public IEnumerable<CalendarItem> TomorrowItems { get; set; }

        public bool HasThisWeekEvents { get; set; }

        public IEnumerable<CalendarItem> ThisWeekEvents { get; set; }

        public bool HasNextWeekEvents { get; set; }

        public IEnumerable<CalendarItem> NextWeekEvents { get; set; }

        public IEnumerable<CalendarItem> ThisMonthEvents { get; set; }

        public bool HasThisMonthEvents { get; set; }

        public bool HasFutureEvents { get; set; }

        public IEnumerable<CalendarItem> FutureEvents { get; set; }
    }
}