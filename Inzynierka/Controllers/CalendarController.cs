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
        // GET: Calendar
        //[Authorize]
        public ActionResult Index()
        {
            var calendarContext = new ScientificWorkContext();

            CalendarListViewModel listViewModel = new CalendarListViewModel();
            string userId = User.Identity.GetUserId();
            var plans = calendarContext.ActivityPlans.Where(o => o.UserId == userId).ToList();

            var calendarItems = plans.Select(plan => new CalendarItem()
            {
                Name = plan.ActivityName, Date = plan.Date.Value.ToString( "dd MMMM yyyy, dddd" ), Id = plan.Id, Type = (int) plan.Type
            }).ToList();
            var dateTimeNowString = DateTime.Now.Date.ToString( "dd MMMM yyyy, dddd" );
            var todayItems = calendarItems.Where(o => o.Date == dateTimeNowString ).ToList();

            listViewModel.HasTodayEvents = todayItems.Any();
            listViewModel.TodayItems = todayItems;

            ViewBag.Items = calendarItems;

            return View( listViewModel );
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
            var viewModel = new CalendarEntryEditViewModel();
            viewModel.Date = DateTime.Now.Date.ToString( "dd-MMM-yyyy" );
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
            var calendarContext = new ScientificWorkContext();

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
            viewModel.Date = plan.Date.Value.ToString( "dd-MMM-yyyy" );
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
            return View();
        }

        [Authorize]
        public ActionResult SaveActivity()
        {
            var request = Request.Form;
            var calendarContext = new ScientificWorkContext();

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
    }
}