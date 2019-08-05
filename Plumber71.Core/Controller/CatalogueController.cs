using Plumber71.Core.Service.ExelPriceProvider.Model;
using Plumber71.Core.Service.PricelistParser;
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
