using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Fleets.Line.Helpers;
using System.Xml.Linq;

namespace Fleets.Line.Controllers
{
    public class InventoryController : Controller
    {
        Promotions Promotions = new Promotions();

        public void Add(string inventoryItem)
        {
            var InventoryItems = Request.Cookies["InventoryItems"];

            if (InventoryItems == null)
            {
                HttpCookie Inventory = SessionInformation.CreateCookie("InventoryItems", inventoryItem);
                Response.Cookies.Add(Inventory);
            }
            else
            {
                InventoryItems.Value = InventoryItems.Value + "+" + inventoryItem;
                Response.Cookies.Add(InventoryItems);
            }
        }

        public void Delete()
        {
            if (Request.Cookies["InventoryItems"] != null)
            {
                var c = new HttpCookie("InventoryItems");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
        }

        public string DeleteRule(string key)
        {
            try
            {
                if (CheckPromotionFormula(key) && key.Length > 0)
                {
                    string RulesPath = Server.MapPath("~/Data/Rules.xml");
                    XDocument XMLDocument2 = XDocument.Load(RulesPath);
                    var Rules = Fleets.Line.Helpers.Inventory.GetRules(XMLDocument2);

                    if (Rules.Any(x => x.Key.ToUpper() == key.ToUpper()))
                    {
                        var node = XMLDocument2.Descendants("Rule").Descendants("Key")
                            .Where(x => x.Value.ToUpper() == key.ToUpper()).First();

                        if(node != null)
                        {
                            node.Parent.Remove();
                        }

                        XMLDocument2.Save(RulesPath);
                        return "This rule" + key.ToUpper() + " has been deleted from the system.";
                    }
                    else
                    {
                        return "This rule doesn't exists. Please refresh and try again.";
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return "This inventory formula cannot be added. Please check inventory items.";
            }

        }


        public string AddRule(string key, string value)
        {
            try
            {               
                if (CheckPromotionFormula(key) && key.Length > 0)
                {
                    var keyValue = Double.Parse(value);

                    string RulesPath = Server.MapPath("~/Data/Rules.xml");
                    XDocument XMLDocument2 = XDocument.Load(RulesPath);
                    var Rules = Fleets.Line.Helpers.Inventory.GetRules(XMLDocument2);

                    if(Rules.Any(x=>x.Key.ToUpper() == key.ToUpper()))
                    {
                        return "This rule already exists. To update delete and add again.";
                    }
                    else
                    {
                        var Rule = new XElement("Rule");
                        Rule.Add(new XElement("Key", key.ToUpper()));
                        Rule.Add(new XElement("Value", keyValue));

                        XMLDocument2.Element("Rules").Add(Rule);
                        XMLDocument2.Save(RulesPath);

                        return "This rule" + key.ToUpper() +" has been saved into the system.";
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return "This inventory formula cannot be added. Please check inventory items.";
            }

        }

        private bool CheckPromotionFormula(string promotionFormula)
        {
            bool FormulaPassedOrFailed = true;
            foreach (var ch in promotionFormula.ToCharArray().ToList())
            {
                if (!(CurrentInventory().Any(x => x.ProductId == ch.ToString() || Char.IsNumber(ch)
                  || Promotions._PromotionOperators.Contains(ch.ToString()) || char.IsWhiteSpace(ch))))
                {
                    FormulaPassedOrFailed = false;
                }
            }
            return FormulaPassedOrFailed;
        }

        private List<Models.Inventory> CurrentInventory()
        {
            string Path = Server.MapPath("~/Data/Inventory.xml");
            XDocument XMLDocument = XDocument.Load(Path);
            return Fleets.Line.Helpers.Inventory.GetInventory(XMLDocument);
        }
    }
}