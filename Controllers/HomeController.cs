using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OkgoPrinter.Models;
using System.IO;
using System.Text;
using OkgoPrinter.Helper;
using Microsoft.AspNetCore.Cors;

namespace OkgoPrinter.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Printer API Version 1.0.0");
        }   
        [HttpPost("printMenu")]
        public IActionResult printMenu([FromBody]printMenu printMenu)
        {
            var _temp = printMenu.data;
            var _all =  printMenu.data.listTransactionDetail;
            var _listB =  printMenu.data.listTransactionDetail.Where(x =>x.category.ToLower()=="beverage").ToList();
            var _listF =  printMenu.data.listTransactionDetail.Where(x =>x.category.ToLower()=="food").ToList();
            
            VMPrinter vmBeverage = new VMPrinter();
            VMPrinter vmFood = new VMPrinter();
            VMPrinter vmAll = new VMPrinter();
            #region print beverage menu
                vmBeverage.transaction = _temp;
                vmBeverage.transaction.listTransactionDetail = _listB;
                vmBeverage.PrinterName=printMenu.beveragePrinter.name;
                PrintHelper.PrintMenu(vmBeverage);
            #endregion
            #region print food menu
                vmFood.transaction = _temp;
                foreach(var item in _listF){
                    var _list = new List<transactionDetail>();
                    _list.Add(item);
                    vmFood.transaction.listTransactionDetail = _list;
                    vmFood.PrinterName = printMenu.foodPrinter.name;
                    PrintHelper.PrintMenu(vmFood);
                }
                
            #endregion
            #region print all
               vmAll.transaction = _temp;
               vmAll.transaction.listTransactionDetail = _all;
               vmAll.PrinterName = printMenu.cashierPrinter.name;
               PrintHelper.PrintMenu(vmAll);
            #endregion

            return Ok();
        }
        [HttpPost("print")]
        public IActionResult print([FromBody] VMPrinter printer)
        {
            PrintHelper.PrintServices(printer);
            return Ok();
        }
         [HttpPost("printreceipt")]
         public  IActionResult PrintReceipt([FromBody] VMPrinter printer)
        {
             PrintHelper.PrintReceipt(printer);
            return Ok();
        }   
    }
}





