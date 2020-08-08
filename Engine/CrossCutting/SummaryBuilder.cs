using CrossCutting.Models;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrossCutting
{
    public class SummaryBuilder
    {
        public static List<ProductMedia> GetFilteredItems(List<ItemFiltrado> items)
        {
            try
            {
                var products = new List<ProductMedia>();

                foreach (var byYear in items.GroupBy(x => x.dhEmi.Year))
                {
                    foreach (var byMonth in byYear.GroupBy(x => x.dhEmi.Month))
                    {
                        foreach (var byNCM in byMonth.GroupBy(x => new { x.cProd, x.NCM }))
                        {
                            var product = new ProductMedia
                            {
                                Name = byNCM.FirstOrDefault()?.xProd,
                                NCM = byNCM.FirstOrDefault()?.NCM,
                                MonthYear = byNCM.FirstOrDefault()?.dhEmi.ToString("MM/yyyy"),
                                LowerValue = byNCM.Any(x => !x.Entrada)
                                    ? byNCM.Where(x => !x.Entrada)?.Min(x => x.vUnCom.Value)
                                    : null,
                                HighestValue = byNCM.Any(x => !x.Entrada)
                                    ? byNCM.Where(x => !x.Entrada)?.Max(x => x.vUnCom.Value)
                                    : null,
                                Media = byNCM.Any(x => !x.Entrada)
                                    ? byNCM.Where(x => !x.Entrada)?.Average(x => x.vUnCom.Value)
                                    : null,
                                TotalResults = byNCM.Select(x => x).Count(),
                                TotalValue = byNCM.Any(x => !x.Entrada)
                                    ? byNCM.Where(x => !x.Entrada)?.Sum(x => x.vUnCom.Value)
                                    : null,
                                MediaEntry = byNCM.Any(x => x.Entrada)
                                    ? byNCM.Where(x => x.Entrada)?.Average(x => x.vUnCom.Value)
                                    : null
                            };

                            products.Add(product);
                        }
                    }
                }

                return products.OrderByDescending(x => x.MediaEntry).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
