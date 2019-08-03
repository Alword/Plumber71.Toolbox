using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Plumber71.Core.Controller
{
    public class ExcelController
    {
        private string originalFileName = $"{Environment.CurrentDirectory}/Resource/price_d5.xls";

        public void ReadExcel()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(originalFileName, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader reader;

                reader = ExcelReaderFactory.CreateReader(stream);
                /// настройка
                var conf = new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true,
                    }
                };

                // считывание
                var dataSet = reader.AsDataSet(conf);

                // первая таблица
                DataTable dataTable = dataSet.Tables[0];
                string test = (string)dataTable.Rows[0][0];
            }
        }
    }
}
Tovar