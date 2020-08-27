using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fleets.Line.Helpers;
using System.Collections.Generic;

namespace Fleets.Line.Test
{
    [TestClass]
    public class FleetsUnitTest
    {
        private Promotions _Promotions = null;
        /// <summary>
        /// Mocking the _PromotionRules
        /// </summary>
        private Dictionary<string, string> _PromotionRules = new Dictionary<string, string>()
        {
            { "A+8C", "50000" },
            { "A+C+B", "50000" }
        };

        private string _InventoryFormula = "2A+9C+B";

        [TestMethod]
        public void CalculatePromotion()
        {
            _Promotions = new Promotions();
            _Promotions.Calculate(_InventoryFormula, _PromotionRules);
        }

        [TestMethod]
        public void CheckFormula()
        {
            _Promotions = new Promotions();
            Assert.IsTrue(_Promotions.CheckPromotionFormula(_InventoryFormula));
        }     
    }
}
