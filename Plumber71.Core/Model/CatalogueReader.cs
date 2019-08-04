using Plumber71.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace Plumber71.Core.Model
{
    public class CatalogueReader
    {
        private DataTable dataTable;
        private Catalogue catalogue = null;
        private CategoryExcel currentCategory = null;
        private ProductExcel currentProduct = null;

        public CatalogueReader(DataTable dataTable)
        {
            this.dataTable = dataTable;
            this.catalogue = new Catalogue();
        }

        public Catalogue HandleData()
        {
            GetInfo($"{dataTable.Rows[0][0]}", catalogue);
            catalogue.Reference = $"{dataTable.Rows[1][0]}";
            HandleCategory(dataTable);
            return catalogue;
        }

        private void HandleCategory(DataTable dataTable)
        {
            // считать каталоги и товары
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                int idResult = GetId(dataTable, i);

                if (idResult == 0) continue; // сткрока не число;

                if (idResult < 1000)
                {
                    HandleCategoryInfo(dataTable, i, idResult);
                }
                else
                {
                    HandleProduct(dataTable, i, idResult);
                }
            }
        }

        private void HandleCategoryInfo(DataTable dataTable, int i, int idResult)
        {
            string cell1 = (string)dataTable.Rows[i][1];
            currentCategory = new CategoryExcel(idResult, cell1);
            catalogue.Categorys.Add(currentCategory);
        }

        private static int GetId(DataTable dataTable, int i)
        {
            string cell0 = dataTable.Rows[i][0].ToString();
            int.TryParse(cell0, out int idResult);
            return idResult;
        }

        private void HandleProduct(DataTable dataTable, int i, int idResult)
        {
            currentProduct = new ProductExcel(idResult)
            {
                Name = (string)dataTable.Rows[i][1],
                Pieces = int.Parse($"{dataTable.Rows[i][2]}"),
                Currency = $"{dataTable.Rows[i][3]}".ToCurrency(),
                TradePriceInCurrency = $"{dataTable.Rows[i][4]}".ToDouble(),
                TradePriceInRubbles = $"{dataTable.Rows[i][5]}".ToDouble(),
                Price7Ka = $"{dataTable.Rows[i][6]}".ToDouble()
            };
            currentCategory.Products.Add(currentProduct);
        }

        private static Catalogue GetInfo(string infoString, Catalogue catalogue)
        {
            string[] infos = infoString.Split('/');

            catalogue.DollarRate = GetCurrency(infos[1]);
            catalogue.EuroRate = GetCurrency(infos[2]);
            catalogue.PriceDate = DateTime.Parse(infoString.Substring(0, 16));

            return catalogue;
        }

        private static double GetCurrency(string info)
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
