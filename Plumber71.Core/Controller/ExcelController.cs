using ExcelDataReader;
using Plumber71.Core.Extentions;
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
        private string Path { get; set; }

        public ExcelController(string path) => this.Path = path;
        public DataSet ReadToEnd()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(Path, FileMode.Open, FileAccess.Read))
            {
                /// читаем exсel
                DataSet dataSet = ReadDataSet(stream);

                return dataSet;
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
    }
}