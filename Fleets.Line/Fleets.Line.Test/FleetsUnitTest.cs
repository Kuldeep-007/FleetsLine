using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fleets.Line.Helpers;
using System.Collections.Generic;

namespace Fleets.Line.Test
{
    [TestClass]
    public class FleetsUnitTest
    {
        /// <summary>
        /// Mocking the _PromotionRules
        /// </summary>
        private Dictionary<string, double> _PromotionRules = new Dictionary<string, double>()
        {
            { "A+C", 50000 }
        };

        private string _PromotionFormula = "A+9C";

        [TestMethod]
        public void CalculatePromotion()
        {
            Promotions.Calculate(_PromotionFormula);
        }

        [TestMethod]
        public void CheckFormula()
        {
            Assert.IsTrue(Promotions.CheckPromotionFormula(_PromotionFormula));
        }     
    }
}
