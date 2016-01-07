using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inzynierka.Models;

namespace Inzynierka.ViewModel.Calendar
{
    public class CalendarEntryEditViewModel
    {
        private DateTime _date;

        [Required]
        [StringLength( 100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string ActivityName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat( DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true )]
        public DateTime Date {
            get
            {
                return ( _date == DateTime.MinValue ) ? DateTime.Now : _date;
            }
            set
            {
                _date = value;
            }
        }


        [Required]
        public ActivityType Type { get; set; }

        public long Id { get; set; }

        public List<SelectListItem> ActivityTypes { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}