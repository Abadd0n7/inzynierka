using Inzynierka.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Inzynierka.Controllers
{
    public class ScientificWorksController : Controller
    {
        private ScientificWorkContext db = new ScientificWorkContext();

        // GET: ScientificWorks
        [Authorize]
        public ActionResult Index()
        {
            return View(db.ScientificWorks.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ScientificWorks work)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ScientificWorks.Add(work);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(work);
            }catch
            {
                return View();
            }
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ScientificWorks work = db.ScientificWorks.Find(id);
            if (work == null)
                return HttpNotFound();
            return View(work);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ScientificWorks work = db.ScientificWorks.Find(id);
            if (work == null)
                return HttpNotFound();

            return View(work);
        }
        [HttpPost]
        public ActionResult Edit(int id, ScientificWorks work)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(work).State=System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(work);
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ScientificWorks work = db.ScientificWorks.Find(id);
            if (work == null)
                return HttpNotFound();
            return View(work);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            
            try
            {  
                ScientificWorks work = db.ScientificWorks.Find(id);      
                db.Entry(work).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        /*public ActionResult SaveActivity()
        {
            var request = Request.Form;
            var editorContext = new ScientificWorkContext();

            var id = request.GetValues("Id");

            ScientificWorks itemToSave = null;

            if (id != null)
            {
                var intId = int.Parse(id[0]);
                itemToSave = editorContext.ScientificWorks.FirstOrDefault(o => o.Id == intId);
            }

            if (itemToSave == null)
            {
                itemToSave = new ScientificWorks();
            }

            itemToSave.Text = request.Get("Text");
            itemToSave.Title = request.Get("Title");
            itemToSave.UserId = request.Get("UserId");
            itemToSave.Created = DateTime.Parse(request.Get("Created"));

            if (itemToSave.Id == 0)
            {
                editorContext.ScientificWorks.Add(itemToSave);
            }
            else
            {
                editorContext.ScientificWorks.AddOrUpdate(itemToSave);
            }
            editorContext.SaveChanges();

            return RedirectToActionPermanent("Index");
        }*/
    }
}