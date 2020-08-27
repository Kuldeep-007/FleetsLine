using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fleets.Line.Models
{
    /// <summary>
    /// Inventory
    /// </summary>
    public class Inventory
    {
        public string ProductId { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}