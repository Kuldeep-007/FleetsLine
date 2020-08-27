using Fleets.Line.Helpers;
using Fleets.Line.Interfaces;
using Fleets.Line.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fleets.Line.Classes
{
    /// <summary>
    /// Add scale operation
    /// </summary>
    public class AddOperation : IOperations
    {
        private List<string> InventoryExpand = null;

        public List<string> ApplyScale(List<Models.Inventory> inventory, string promotionFormula)
        {
            try
            {
                InventoryExpand = new List<string>();
                var PromotionFormula = promotionFormula.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);

                foreach(var promotion in PromotionFormula)
                {
                    if(inventory.Select(x => x.ProductId).Any(x => promotion == x))
                    {
                        InventoryExpand.Add(promotion);
                    }
                    else if (promotion.Any(x => Char.IsNumber(x)) && inventory.Select(x=>x.ProductId).Any(x => promotion.EndsWith(x)))
                    {
                        var scale = StringHelpers.GetDigitsInString(promotion);
                        for(int i=0; i<scale; i++)
                        {
                            InventoryExpand.Add(StringHelpers.GetCharactersInString(promotion));
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                return InventoryExpand;

            }
            catch(Exception ex)
            {
                throw new Exception("Invalid inventory promotion formula. Please try again.");
            }
        }

        public List<OperationResult> ApplyRules(List<Models.Inventory> inventory, string promotionFormula, Dictionary<string, string> promotionRules, List<string> inventoryScale)
        {
            List<OperationResult> OperationResult = new List<Models.OperationResult>();
            try
            {
                var PromotionRules = new Dictionary<string, double>();
                foreach (var rule in promotionRules)
                {
                    PromotionRules.Add(rule.Key, StringHelpers.ResolveValue(rule.Value));
                }

                var PromotionRulesSorted = PromotionRules.OrderByDescending(x => x.Value);
                foreach (var rule in PromotionRulesSorted)
                {
                    var PromotionRule = rule.Key;
                    var PromotionScale = ApplyScale(inventory, PromotionRule);

                    List<string> PresentScale = new List<string>();
                    bool IsScaleApplicable = true;
                    foreach(var scale in PromotionScale)
                    {
                        if (!inventoryScale.Any(x => x == scale))
                        {
                            IsScaleApplicable = false;
                            break;
                        }
                        else
                        {
                            inventoryScale.Remove(scale);                            
                        }
                        PresentScale.Add(scale);
                    }

                    if (IsScaleApplicable)
                    {
                        OperationResult.Add(new Models.OperationResult()
                        {
                            TotalValue = rule.Value,
                            Operation = rule.Key
                        });
                    }
                    else
                    {
                        //If scale not applicable then put it back on inventory list
                        inventoryScale.AddRange(PresentScale);
                    }
                }

                //Add prices for residual inventory items (without promotions)
                foreach(var item in inventoryScale)
                {
                    OperationResult.Add(new Models.OperationResult()
                    {
                        TotalValue = inventory.Where(x=>x.ProductId == item).First().Price,
                        Operation = null
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot apply promotion rules. Please try again.");
            }
            return OperationResult;
        }
    }
}