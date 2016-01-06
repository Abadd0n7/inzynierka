using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inzynierka.ViewModel.Calendar
{
    public class CalendarListViewModel
    {
        public bool HasTodayEvents { get; set; }

        public IEnumerable<CalendarItem> TodayItems { get; set; }
    }
}