using Plumber71.Core.Extentions;
using Plumber71.Core.Service.ExelPriceProvider.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Plumber71.Core.Service.ExelPriceProvider
{
    public class ExcelPriceProvider
    {
        private readonly DataTable dataTable;
        private readonly ExcelPricelist excelPricelist = null;
        private ExcelCategory currentCategory = null;
        private ExcelProduct currentProduct = null;

        public ExcelPriceProvider(DataTable dataTable)
        {
            this.dataTable = dataTable;
            this.excelPricelist = new ExcelPricelist();
        }

        /// <summary>
        /// Parse excel DataTable to ExcelProduct
        /// </summary>
        /// <returns></returns>
        public ExcelPricelist Parse()
        {
            SetCurrencyInfo($"{dataTable.Rows[0][0]}", excelPricelist);
            excelPricelist.Reference = $"{dataTable.Rows[1][0]}";
            ParseCategory(dataTable);
            return excelPricelist;
        }

        private void ParseCategory(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                int idResult = GetId(dataRow);
                if (idResult < 1000)
                {
                    HandleCategoryInfo(dataRow, idResult);
                }
                else
                {
                    ParseProduct(dataRow, idResult);
                }
            }
        }

        private void HandleCategoryInfo(DataRow dataRow, int idResult)
        {
            string categoryName = $"{dataRow[1]}";
            currentCategory = new ExcelCategory(idResult, categoryName);
            excelPricelist.Categorys.Add(currentCategory);
        }

        private static int GetId(DataRow dataRow)
        {
            string IdString = $"{dataRow[0]}";
            int.TryParse(IdString, out int id);
            return id;
        }

        private void ParseProduct(DataRow dataRow, int idResult)
        {
            currentProduct = new ExcelProduct(idResult)
            {
                Name = $"{dataRow[1]}",
                Pieces = dataRow[2].ToInt(),
                Currency = dataRow[3].ToCurrency(),
                TradePriceInCurrency = dataRow[4].ToDouble(),
                TradePriceInRubbles = dataRow[5].ToDouble(),
                Price7Ka = dataRow[6].ToDouble()
            };
            currentCategory.Products.Add(currentProduct);
        }

        private static ExcelPricelist SetCurrencyInfo(string infoString, ExcelPricelist catalogue)
        {
            string[] infos = infoString.Split('/');

            catalogue.DollarRate = ParseCurrencyValue(infos[1]);
            catalogue.EuroRate = ParseCurrencyValue(infos[2]);
            catalogue.PriceDate = DateTime.Parse(infoString.Substring(0, 16));

            return catalogue;
        }

        private static double ParseCurrencyValue(string info)
        {
            info = info.Trim();
            string[] buffer = info.Split(':');
            int substringIndex = info.IndexOf(buffer[0]);
            string currencyValueText = info.Substring(substringIndex + buffer[0].Length + 1).Replace(".", ",");
            double.TryParse(currencyValueText, out double currencyValue);
            return currencyValue;
        }
    }
}
