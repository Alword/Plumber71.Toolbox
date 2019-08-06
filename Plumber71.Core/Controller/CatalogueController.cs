using Plumber71.Core.Service.ExcelDataSetReader;
using Plumber71.Core.Service.PricelisDataSetParser;
using Plumber71.Core.Service.PricelisDataSetParser.Model;
using System.Data;

namespace Plumber71.Core.Controller
{
    public class PricelistController
    {
        private ExcelController excelController { get; set; }
        private PricelistParser excelPriseProvider { get; set; }
        public PricelistController(string path)
        {
            excelController = new ExcelController(path);
        }
        public Priselist ParseCatalogue()
        {
            DataSet dataSet = excelController.ReadToEnd();
            DataTable dataTable = dataSet.Tables[0];
            excelPriseProvider = new PricelistParser(dataTable);
            return excelPriseProvider.Parse();
        }
    }
}
