using OkgoPrinter.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OkgoPrinter.Models
{
    public class printMenu
    {
        public transaction data {get;set;}
        public printerSetup foodPrinter {get;set;}
        public printerSetup beveragePrinter {get;set;}
        public printerSetup cashierPrinter {get;set;}
    }
}
