using Plumber71.Core.Service.ExelPriceProvider;
using Plumber71.Core.Service.ExelPriceProvider.Model;
using System.Data;

namespace Plumber71.Core.Controller
{
    public class PricelistController
    {
        private ExcelController excelController { get; set; }
        private ExcelPriceProvider excelPriseProvider { get; set; }
        public PricelistController(string path)
        {
            excelController = new ExcelController(path);
        }
        public ExcelPricelist ParseCatalogue()
        {
            DataSet dataSet = excelController.ReadToEnd();
            DataTable dataTable = dataSet.Tables[0];
            excelPriseProvider = new ExcelPriceProvider(dataTable);
            return excelPriseProvider.Parse();
        }
    }
}
