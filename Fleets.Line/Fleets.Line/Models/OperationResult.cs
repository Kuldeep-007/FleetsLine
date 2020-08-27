using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fleets.Line.Models
{
    /// <summary>
    /// OperationResult
    /// </summary>
    public class OperationResult
    {
        public double TotalValue { get; set; }
        public string Operation { get; set; }
        public bool IsPromotion { get; set; }
    }
}