using Plumber71.Core.Service.ExcelDataSetReader;
using Plumber71.Core.Service.PricelisDataSetParser;
using Plumber71.Core.Service.PricelisDataSetParser.Model;
using System.Data;

namespace Plumber71.Core.Controller
{
    public class ExcelPricelistController
    {
        private ExcelController ExcelController { get; set; }
        private PricelistParser ExcelPriseProvider { get; set; }
        public ExcelPricelistController(string path)
        {
            ExcelController = new ExcelController(path);
        }
        public Priselist GetExcelPricelist()
        {
            DataSet dataSet = ExcelController.ReadToEnd();
            DataTable dataTable = dataSet.Tables[0];
            ExcelPriseProvider = new PricelistParser(dataTable);
            return ExcelPriseProvider.Parse();
        }
    }
}
