using ExcelDataReader;
using Plumber71.Core.Extentions;
using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Plumber71.Core.Service.ExcelDataSetReader
{
    /// <summary>
    /// This class is just for read excel file into DataTable
    /// </summary>
    public class ExcelController
    {
        private string Path { get; set; }

        public ExcelController(string path) => Path = path;
        public DataSet ReadToEnd()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(Path, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                return reader.AsDataSet();
            }
        }
    }
}