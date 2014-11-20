using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DudeFinder.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(double lat,double lng)
        {
            ViewBag.PartyUrl = Url.Action("Index");
            ViewBag.Lat = lat;
            ViewBag.Lng = lng;
            return View();
        }
    }
}