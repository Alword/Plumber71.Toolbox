using Plumber71.Core.Controller;
using System;

namespace Plumber71.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("}|{OPA");
            ExcelController excelController = new ExcelController();
            excelController.ReadExcel();
            Console.ReadLine();
        }
    }
}
