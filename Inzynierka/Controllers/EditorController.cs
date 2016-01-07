using Inzynierka.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inzynierka.Controllers
{
    public class EditorController : Controller
    {
        // GET: Editor
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View(new Editor());
        }
        public ActionResult SaveActivity()
        {
            var request = Request.Form;
            var editorContext = new ScientificWorkContext();

            var id = request.GetValues("Id");

            WorkPlan itemToSave = null;

            if (id != null)
            {
                var intId = int.Parse(id[0]);
                itemToSave = editorContext.WorkPlans.FirstOrDefault(o => o.Id == intId);
            }

            if (itemToSave == null)
            {
                itemToSave = new WorkPlan();
            }

            itemToSave.Body = request.Get("Body");
            itemToSave.Title = request.Get("Title");
            itemToSave.UserId = request.Get("UserId");

            if (itemToSave.Id == 0)
            {
                editorContext.WorkPlans.Add(itemToSave);
            }
            else
            {
                editorContext.WorkPlans.AddOrUpdate(itemToSave);
            }
            editorContext.SaveChanges();

            return RedirectToActionPermanent("Index");
        }
    }
}