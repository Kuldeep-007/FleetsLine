using Fleets.Line.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleets.Line.Interfaces
{
    /// <summary>
    /// IOperations
    /// </summary>
    public interface IOperations
    {
        List<string> ApplyScale(List<Models.Inventory> inventory, string promotionFormula);
        List<OperationResult> ApplyRules(List<Models.Inventory> inventory, string promotionFormula, Dictionary<string, string> promotionRules, List<string> inventoryScale);
    }
}
