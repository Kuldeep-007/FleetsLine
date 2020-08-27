using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fleets.Line.Models
{
    /// <summary>
    /// Cart
    /// </summary>
    public class Cart
    {
        public List<Inventory> AddedInventory { get; set; }
        public List<OperationResult> OperationResult { get; set; }
    }
}