using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inzynierka.Models;
using Inzynierka.ViewModel.Calendar;
using Microsoft.AspNet.Identity;

namespace Inzynierka.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        [Authorize]
        public ActionResult Index()
        {
            var calendarItems = new List<CalendarItem>();

            var calendarContext = new ScientificWorkContext();
            string userId = User.Identity.GetUserId();
            var plans = calendarContext.ActivityPlans.Where(o => o.UserId == userId).ToList();

            foreach ( var plan in plans )
            {
                calendarItems.Add(new CalendarItem()
                {
                    Name = plan.ActivityName,
                    Date = plan.Date,
                    Id = plan.Id
                });
            }

            ViewBag.Items = calendarItems;
            return View();
        }

        // GET: Calendar/{0}
        [Authorize]
        public ActionResult Details()
        {
            return View();
        }

        // POST: Calendar
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }

        [Authorize]
        public ActionResult Delete()
        {
            return View();
        }
    }
}