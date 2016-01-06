using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
        ScientificWorkContext calendarContext = new ScientificWorkContext();
        // GET: Calendar
        //[Authorize]
        public ActionResult Index()
        {
            CalendarListViewModel listViewModel = new CalendarListViewModel();
            string userId = User.Identity.GetUserId();
            var plans = calendarContext.ActivityPlans.Where(o => o.UserId == userId).ToList();

            var calendarItems = plans.Select(plan => new CalendarItem()
            {
                Name = plan.ActivityName, Date = plan.Date.Value.ToString( "dd MMMM yyyy, dddd" ), Id = plan.Id, Type = (int) plan.Type
            }).ToList();
            var dateTimeNowString = DateTime.Now.Date.ToString( "dd MMMM yyyy, dddd" );
            var tomorrowString = DateTime.Now.Date.AddDays(1);
            var weekString = DateTime.Now.Date.AddDays( 7 );
            var nextWeekString = DateTime.Now.Date.AddDays( 14 );
            var thisMonthString = DateTime.Now.Date.AddDays( 30 );
            var todayItems = calendarItems.Where(o => o.Date == dateTimeNowString ).ToList();

            var pastItems = calendarItems.Where(o => DateTime.Parse(o.Date) < DateTime.Now.Date).ToList();

            var tomorrowItems = calendarItems.Where(o => DateTime.Parse(o.Date) == tomorrowString).ToList();

            var thisWeek = calendarItems.Where(o => DateTime.Parse(o.Date) > tomorrowString && DateTime.Parse(o.Date) <= weekString).OrderByDescending(o => o.Date).ToList();
            var nextWeek = calendarItems.Where( o => DateTime.Parse( o.Date ) > weekString && DateTime.Parse( o.Date ) <= nextWeekString ).OrderByDescending( o => o.Date ).ToList();
            var thisMonthWeek = calendarItems.Where( o => DateTime.Parse( o.Date ) > nextWeekString && DateTime.Parse( o.Date ) <= thisMonthString ).OrderByDescending( o => o.Date ).ToList();
            var futureWeek = calendarItems.Where( o => DateTime.Parse( o.Date ) > thisMonthString ).OrderByDescending( o => o.Date ).ToList();
            
            listViewModel.HasTodayEvents = todayItems.Any();
            listViewModel.TodayItems = todayItems;

            listViewModel.HasPastEvents = pastItems.Any();
            listViewModel.PastItems = pastItems;

            listViewModel.HasTomorrowItems = tomorrowItems.Any();
            listViewModel.TomorrowItems = tomorrowItems;

            listViewModel.HasThisWeekEvents = thisWeek.Any();
            listViewModel.ThisWeekEvents = thisWeek;

            listViewModel.NextWeekEvents = nextWeek;
            listViewModel.HasNextWeekEvents = nextWeek.Any();

            listViewModel.ThisMonthEvents = thisMonthWeek;
            listViewModel.HasThisMonthEvents = thisMonthWeek.Any();

            listViewModel.FutureEvents = futureWeek;
            listViewModel.HasFutureEvents = futureWeek.Any();

            ViewBag.Items = calendarItems;

            return View( listViewModel );
        }
        
        // GET: Calendar/Details/{0}
        [Authorize]
        public ActionResult Details()
        {
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

            if ( plan.Date.Value.Date >= DateTime.Now.Date )
            {
                ViewBag.CanEdit = true;
            }
            else
            {
                ViewBag.CanEdit = false;
            }

            return View( plan );
        }

        public ActionResult ActivityError()
        {
            return View();
        }

        // POST: Calendar
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new CalendarEntryEditViewModel();
            viewModel.Date = DateTime.Now.Date;
            viewModel.ActivityTypes = new List<SelectListItem>();
            viewModel.Type = ActivityType.Deadline;
            string userId = User.Identity.GetUserId();
            viewModel.UserId = userId;

            CreateSelectItemForActivityType( viewModel.ActivityTypes, Resources.Strings.Resource.PublicationText, "1" );
            CreateSelectItemForActivityType( viewModel.ActivityTypes, Resources.Strings.Resource.DeadlineText, "2" );
            CreateSelectItemForActivityType( viewModel.ActivityTypes, Resources.Strings.Resource.ReviewText, "3" );
            CreateSelectItemForActivityType( viewModel.ActivityTypes, Resources.Strings.Resource.StartWritingText, "4" );

            return View( viewModel );
        }

        [Authorize]
        public ActionResult Edit()
        {
            var id = RouteData.Values.ContainsKey( "id" ) ? RouteData.Values["id"].ToString() : null;
            if ( String.IsNullOrEmpty( id ) )
            {
                return RedirectToActionPermanent( "ActivityError" );
            }

            var data = int.Parse( id );

            var plan = calendarContext.ActivityPlans.FirstOrDefault( o => o.Id == data );

            if ( plan == null )
            {
                return RedirectToActionPermanent( "ActivityError" );
            }

            var viewModel = new CalendarEntryEditViewModel();
            viewModel.ActivityName = plan.ActivityName;
            viewModel.Date = plan.Date.Value;
            viewModel.ActivityTypes = new List<SelectListItem>();
            viewModel.Id = plan.Id;
            viewModel.Type = plan.Type;
            viewModel.Description = plan.Description;
            viewModel.UserId = plan.UserId;
            
            CreateSelectItemForActivityType( viewModel.ActivityTypes , Resources.Strings.Resource.PublicationText, "1" );
            CreateSelectItemForActivityType( viewModel.ActivityTypes, Resources.Strings.Resource.DeadlineText, "2" );
            CreateSelectItemForActivityType( viewModel.ActivityTypes, Resources.Strings.Resource.ReviewText, "3" );
            CreateSelectItemForActivityType( viewModel.ActivityTypes, Resources.Strings.Resource.StartWritingText, "4" );
           
            return View( viewModel );
        }

        private void CreateSelectItemForActivityType( IList<SelectListItem> elements, string caption, string value )
        {
            var  newItem = new SelectListItem();
            newItem.Value = value;
            newItem.Text = caption;
            elements.Add(newItem);
        }

        [Authorize]
        public ActionResult Delete(string parameter)
        {
            var id = RouteData.Values.ContainsKey( "id" ) ? RouteData.Values["id"].ToString() : null;
            if ( String.IsNullOrEmpty( id ) )
            {
                return RedirectToActionPermanent( "ActivityError" );
            }

            var data = int.Parse( id );

            var plan = calendarContext.ActivityPlans.FirstOrDefault( o => o.Id == data );

            if ( plan == null )
            {
                return RedirectToActionPermanent( "ActivityError" );
            }
            
            return View(plan);
        }

        [Authorize]
        public ActionResult SaveActivity()
        {
            var request = Request.Form;
            
            var id = request.GetValues("id");

            ActivityPlan itemToSave = null;

            if ( id != null )
            {
                var intId = int.Parse( id[0] );
                itemToSave = calendarContext.ActivityPlans.FirstOrDefault( o => o.Id == intId );
            }

            if ( itemToSave == null )
            {
                itemToSave = new ActivityPlan();
            }

            itemToSave.Description = request.Get("Description");
            itemToSave.Date = DateTime.Parse(request.Get("Date"));
            itemToSave.Type = (ActivityType) Enum.Parse(typeof(ActivityType), request.Get("Type"));
            itemToSave.ActivityName = request.Get("ActivityName");
            itemToSave.UserId = request.Get("UserId");

            if ( itemToSave.Id == 0 )
            {
                calendarContext.ActivityPlans.Add( itemToSave );
            }
            else
            {
                calendarContext.ActivityPlans.AddOrUpdate( itemToSave );
            }
            calendarContext.SaveChanges();

            return RedirectToActionPermanent( "Index" );
        }

        [Authorize]
        public ActionResult DeleteActivity()
        {
            var request = Request.Form;

            var id = request.Get( "id" );
            var intId = int.Parse( id );
            var item = calendarContext.ActivityPlans.FirstOrDefault( o => o.Id == intId );

            if ( item != null )
            {
                calendarContext.ActivityPlans.Remove(item);
            }
            calendarContext.SaveChanges();

            return RedirectToActionPermanent( "Index" );
        }
    }
}