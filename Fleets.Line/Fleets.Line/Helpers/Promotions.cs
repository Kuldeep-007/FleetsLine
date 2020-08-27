using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace Fleets.Line.Helpers
{
    /// <summary>
    /// Promotions
    /// </summary>
    public static class Promotions
    {
        public static readonly List<string> PromotionOperators = new List<string>() { "+" };

        /// <summary>
        /// Calculate
        /// </summary>
        /// <param name="PromotionFormula"></param>
        /// <returns></returns>
        public static double Calculate(string PromotionFormula)
        {
            try
            {
                if (CheckPromotionFormula(PromotionFormula))
                {
                    var InventoryItems = CurrentInventory();


                }
                else
                {
                    throw new Exception("This is an invalid formula/expression. Please check again.");
                }
                return 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private static List<Models.Inventory> CurrentInventory()
        {
            XDocument XMLDocument = XDocument.Load(@"..\..\..\Fleets.Line\Data\Inventory.xml");
            return Fleets.Line.Helpers.Inventory.GetInventory(XMLDocument);
        }

        public static bool CheckPromotionFormula(string PromotionFormula)
        {
            bool FormulaPassedOrFailed = true;
            foreach( var ch in PromotionFormula.ToCharArray().ToList())
            {
                if(!(CurrentInventory().Any(x => x.ProductId == ch.ToString() || Char.IsNumber(ch)
                 || PromotionOperators.Contains(ch.ToString()) || char.IsWhiteSpace(ch))))
                {
                    FormulaPassedOrFailed =  false;
                }
            }
            return FormulaPassedOrFailed;
        }
    }
}