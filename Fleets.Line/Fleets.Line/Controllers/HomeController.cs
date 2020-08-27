using Fleets.Line.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace Fleets.Line.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Land on home page and choose how to proceed further:
        /// => As a Fleet Manager for FleetsLine
        /// => Or as a Customer on FleetsLine portal
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //Load XML and create inventory object
            string Path = Server.MapPath("~/Data/Inventory.xml");
            XDocument XMLDocument = XDocument.Load(Path);
            var Inventory = Fleets.Line.Helpers.Inventory.GetInventory(XMLDocument);

            return View(Inventory);
        }
    }
}