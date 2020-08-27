using Fleets.Line.Classes;
using Fleets.Line.Helpers;
using Fleets.Line.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            //Load XML and create inventory object
            string Path = Server.MapPath("~/Data/Inventory.xml");
            XDocument XMLDocument = XDocument.Load(Path);
            var Inventory = Fleets.Line.Helpers.Inventory.GetInventory(XMLDocument);

            FleetModel FleetModel = new FleetModel();
            FleetModel.Inventory = Inventory;

            string RulesPath = Server.MapPath("~/Data/Rules.xml");
            XDocument XMLDocument2 = XDocument.Load(RulesPath);
            var Rules = Fleets.Line.Helpers.Inventory.GetRules(XMLDocument2);
            FleetModel.Rules = Rules;

            Cart Cart = await BuildCartAsync(XMLDocument, Rules, Inventory);
            FleetModel.Cart = Cart;

            return View(FleetModel);
        }

        private Task<Cart> BuildCartAsync(XDocument XMLDocument, List<Rules> Rules, List<Models.Inventory> ItemsInventory)
        {
            Cart Cart = new Cart();
            try
            {
                Cart.AddedInventory = new List<Models.Inventory>();
                var InventoryItems = Request.Cookies["InventoryItems"];
                if (InventoryItems != null)
                {
                    var ItemCodes = InventoryItems.Value.Split(new string[] { "+" }, StringSplitOptions.None);
                    foreach (var item in ItemCodes)
                    {
                        var Inventory = Fleets.Line.Helpers.Inventory.GetInventoryItem(XMLDocument, item);
                        if(!Cart.AddedInventory.Any(x=>x.ProductId.ToUpper() == item.ToUpper()))
                        {
                            if (Inventory != null)
                            {
                                Cart.AddedInventory.Add(Inventory);
                            }
                        }
                        else
                        {
                            Cart.AddedInventory.Where(x => x.ProductId.ToUpper() == item.ToUpper()).ToList().ForEach(s => s.Quantity += 1);
                        }
                    }

                    //Add promotion rules
                    Dictionary<string, string> PromotionRules = new Dictionary<string, string>();
                    foreach(var rule in Rules)
                    {
                        PromotionRules.Add(rule.Key, rule.Value);
                    }

                    //Call promotion operations
                    AddOperation Operations = new AddOperation();
                    var InventoryScales = Operations.ApplyScale(ItemsInventory, InventoryItems.Value);
                    if (InventoryScales.Any())
                    {
                        var InventoryRules = Operations.ApplyRules(ItemsInventory, InventoryItems.Value, PromotionRules, InventoryScales);
                        Cart.OperationResult = InventoryRules;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.FromResult(Cart);
        }
    }
}