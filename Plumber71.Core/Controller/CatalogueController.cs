using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Plumber71.Core.Controller
{
    public class CatalogueController
    {
        private ExcelController excelController { get; set; }
        private CatalogueReader catalogueReader { get; set; }
        public CatalogueController(string path)
        {
            excelController = new ExcelController(path);
        }
        public Catalogue ParseCatalogue()
        {
            DataSet dataSet = excelController.ReadToEnd();
            DataTable dataTable = dataSet.Tables[0];
            catalogueReader = new CatalogueReader(dataTable);
            return catalogueReader.HandleData();
        }
    }
}
