// using OkgoPrinter.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OkgoPrinter.Models
{
    public class VMPrinter
    {
        public string PrinterName {get;set;}
        public transaction transaction {get;set;}
        // public string UserName {get;set;}
        // public string Phone {get;set;}
    }
}
