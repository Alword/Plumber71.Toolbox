using ExcelDataReader;
using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Plumber71.Core.Controller
{
    public class ExcelController
    {
        public void ReadExcel(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                /// читаем exсel
                DataSet dataSet = ReadDataSet(stream);
                // первая таблица
                ReadData(dataSet);
            }
        }

        private static void ReadData(DataSet dataSet)
        {
            DataTable dataTable = dataSet.Tables[0];

            Catalogue catalogue = new Catalogue();
            Category currentCategory = null;

            //GetInfo()

            // считать каталоги и товары
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string cell0 = dataTable.Rows[i][0].ToString();

                int.TryParse(cell0, out int idResult);

                if (idResult == 0) continue; // сткрока не число;

                if (idResult < 1000)
                {
                    string cell1 = (string)dataTable.Rows[i][1];
                    currentCategory = new Category(idResult, cell1);
                    catalogue.Categorys.Add(currentCategory);
                }
                else
                {
                    Product product = new Product(idResult)
                    {
                        Name = (string)dataTable.Rows[i][1],
                        Pieces = (int)dataTable.Rows[i][2],
                        TradePriceInCurrency = (double)dataTable.Rows[i][4],
                        TradePriceInRubbles = (double)dataTable.Rows[i][5],
                        Price7Ka = (double)dataTable.Rows[i][6],
                    };

                    // Currency= (int)dataTable.Rows[i][1]
                }

                //Код - выфв
                //100 - каталог 0 2 тгдд
                //24959 - товар
            }
        }

        private static DataSet ReadDataSet(FileStream stream)
        {
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
            /// настройка
            var conf = new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true,
                }
            };

            /// считывание
            var dataSet = reader.AsDataSet(conf);
            return dataSet;
        }
        private static Catalogue GetInfo(string infoString, Catalogue catalogue)
        {
            //01.08.2019 09:06 (ЦБ) / USD:63.42 / EUR:70.74
            //catalogue.DollarRate = 63.42
            //catalogue.EuroRate = 70.74
            //catalogue.PriceDate = 01.08.2019 09:06

            string[] infos = infoString.Split('/');
            //01.08.2019 09:06 (ЦБ)
            //USD:63.42
            //EUR:70.74



            int usdIndex = infoString.IndexOf("USD:");
            int eurIndex = infoString.IndexOf("EUR:");

            catalogue.DollarRate = double.Parse(infoString.Substring(usdIndex, infoString.Length - 12));
            catalogue.EuroRate = double.Parse(infoString.Substring(eurIndex));
            catalogue.PriceDate = DateTime.Parse(infoString.Substring(0, 16));

            return catalogue;
        }

        //USD:63.42
        //EUR:70.74
        private static double GetCurrency(string info)
        {
            string buffer = info.Substring(':');
            string currencyValueText = info.Substring(buffer[0]);
            double.TryParse(currencyValueText,out double currencyValue);
            return currencyValue;
        }
    }
}