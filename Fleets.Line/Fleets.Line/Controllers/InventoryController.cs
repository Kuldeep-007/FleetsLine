using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Fleets.Line.Helpers;

namespace Fleets.Line.Controllers
{
    public class InventoryController : Controller
    {       
        public ActionResult Add(string inventoryItem)
        {

            var d = Request.Cookies["InventoryItems"];

            HttpCookie SessionId = SessionInformation.CreateCookie("ConfiguratorSessionId","");
            Response.Cookies.Add(SessionId);

            return null;
        }

        public ActionResult Delete(string inventoryItem)
        {
            return View();
        }
    }
}