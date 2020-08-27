using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fleets.Line.Models
{
    /// <summary>
    /// FleetModel
    /// </summary>
    public class FleetModel
    {
        public List<Inventory> Inventory { get; set; }
        public List<Rules> Rules { get; set; }
        public Cart Cart { get; set; }
    }
}