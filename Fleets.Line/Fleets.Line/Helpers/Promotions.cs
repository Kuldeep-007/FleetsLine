using Fleets.Line.Classes;
using Fleets.Line.Interfaces;
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
    public class Promotions
    {
        private readonly List<string> _PromotionOperators = new List<string>() { "+" };
        private IOperations _AddScaleOperations = null;

        public Promotions()
        {
            _AddScaleOperations = new AddOperation();
        }

        /// <summary>
        /// Calculate
        /// </summary>
        /// <param name="promotionFormula"></param>
        /// <param name="promotionRules"></param>
        /// <returns></returns>
        public double Calculate(string promotionFormula, Dictionary<string, string> promotionRules)
        {
            try
            {
                if (CheckPromotionFormula(promotionFormula))
                {
                    var InventoryScales = _AddScaleOperations.ApplyScale(CurrentInventory(), promotionFormula);
                    if (InventoryScales.Any())
                    {
                        var InventoryRules = _AddScaleOperations.ApplyRules(CurrentInventory(), promotionFormula, promotionRules, InventoryScales);
                    }
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

        /// <summary>
        /// Get current inventory
        /// </summary>
        /// <returns></returns>
        private static List<Models.Inventory> CurrentInventory()
        {
            XDocument XMLDocument = XDocument.Load(@"..\..\..\Fleets.Line\Data\Inventory.xml");
            return Fleets.Line.Helpers.Inventory.GetInventory(XMLDocument);
        }

        /// <summary>
        /// Check for promotion rules on the cart inventory
        /// </summary>
        /// <param name="promotionFormula"></param>
        /// <returns></returns>
        public bool CheckPromotionFormula(string promotionFormula)
        {
            bool FormulaPassedOrFailed = true;
            foreach( var ch in promotionFormula.ToCharArray().ToList())
            {
                if(!(CurrentInventory().Any(x => x.ProductId == ch.ToString() || Char.IsNumber(ch)
                 || _PromotionOperators.Contains(ch.ToString()) || char.IsWhiteSpace(ch))))
                {
                    FormulaPassedOrFailed =  false;
                }
            }
            return FormulaPassedOrFailed;
        }
    }
}