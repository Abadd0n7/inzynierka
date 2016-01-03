using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inzynierka.Controllers
{
    public class WorksController : Controller
    {
        // GET: Works
		[Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}