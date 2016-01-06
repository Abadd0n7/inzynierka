using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inzynierka.ViewModel.Calendar
{
    public class CalendarItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime? Date { get; set; }
    }
}