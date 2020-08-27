using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Fleets.Line.Helpers
{
    /// <summary>
    /// Inventory
    /// </summary>
    public static class Inventory
    {
        public static List<Fleets.Line.Models.Inventory> GetInventory(XDocument xDocument)
        {
            List<Fleets.Line.Models.Inventory> Inventory = new List<Fleets.Line.Models.Inventory>();
            foreach (XElement xe in xDocument.Descendants("Item"))
            {
                Inventory.Add(new Models.Inventory()
                {
                    ProductId = xe.Element("Id").Value,
                    ProductImage = xe.Element("Image").Value,
                    Description = xe.Element("Description").Value,
                    Currency = xe.Element("Currency").Value,
                    Price = Convert.ToDouble(xe.Element("Price").Value)
                });
            }
            return Inventory;
        }

        public static List<Fleets.Line.Models.Rules> GetRules(XDocument xDocument)
        {
            List<Fleets.Line.Models.Rules> Rules = new List<Fleets.Line.Models.Rules>();
            foreach (XElement xe in xDocument.Descendants("Rule"))
            {
                Rules.Add(new Models.Rules()
                {
                    Key = xe.Element("Key").Value,
                    Value = xe.Element("Value").Value,
                });
            }
            return Rules;
        }

        public static Fleets.Line.Models.Inventory GetInventoryItem(XDocument xDocument, string code)
        {
            Fleets.Line.Models.Inventory Inventory = null;
            foreach (XElement xe in xDocument.Descendants("Item"))
            {
                if (xe.Element("Id").Value.ToUpper() == code.ToUpper())
                {
                    Inventory = new Models.Inventory()
                    {
                        ProductId = xe.Element("Id").Value,
                        ProductImage = xe.Element("Image").Value,
                        Description = xe.Element("Description").Value,
                        Currency = xe.Element("Currency").Value,
                        Price = Convert.ToDouble(xe.Element("Price").Value)
                    };
                }
            }
            return Inventory;
        }
    }
}