using Plumber71.Core.Controller;
using System;

namespace Plumber71.TestConsole
{
    class Program
    {
        private static string originalFileName = $"{Environment.CurrentDirectory}/Resource/price_d5.xls";
        static void Main(string[] args)
        {
            Console.WriteLine("}|{OPA");
            ExcelController excelController = new ExcelController();
            excelController.ReadExcel(originalFileName);
            Console.ReadLine();
        }
    }
}
