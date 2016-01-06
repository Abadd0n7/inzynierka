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
        //[Authorize]
        public ActionResult Index()
        {
            var calendarContext = new ScientificWorkContext();
            string userId = User.Identity.GetUserId();
            var plans = calendarContext.ActivityPlans.Where(o => o.UserId == userId).ToList();

            var calendarItems = plans.Select(plan => new CalendarItem()
            {
                Name = plan.ActivityName, Date = plan.Date.Value.ToString( "dd MMMM yyyy, dddd" ), Id = plan.Id
            }).ToList();
            var dateTimeNowString = DateTime.Now.Date.ToString( "dd MMMM yyyy, dddd" );
            var todayItems = calendarItems.Where(o => o.Date == dateTimeNowString );

            ViewBag.HasTodayEvents = todayItems.Any();
            ViewBag.TodayItems = todayItems;

            ViewBag.Items = calendarItems;
            return View();
        }
        
        // GET: Calendar/Details/{0}
        [Authorize]
        public ActionResult Details()
        {
            var calendarContext = new ScientificWorkContext();

            var id = RouteData.Values.ContainsKey("id") ? RouteData.Values["id"].ToString() : null;
            if ( String.IsNullOrEmpty(id) )
            {
                return RedirectToActionPermanent( "ActivityError" );
            }

            var data = int.Parse(id);
            
            var plan = calendarContext.ActivityPlans.FirstOrDefault(o => o.Id == data);

            if ( plan == null )
            {
                return RedirectToActionPermanent( "ActivityError" );
            }

            ViewBag.Context = plan;

            return View();
        }

        public ActionResult ActivityError()
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
        public ActionResult Edit(string parameter)
        {
            return View();
        }

        [Authorize]
        public ActionResult Delete(string parameter)
        {
            return View();
        }
    }
}