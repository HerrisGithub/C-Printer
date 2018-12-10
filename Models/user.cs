using OkgoPrinter.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OkgoPrinter.Models
{
    public class user
    {
      [Key]
        public int id { get; set; }
        public int ? merchantId { get; set; }
        public string type { get; set; }
        public decimal balance { get; set; }   
        public decimal limitBalance {get; set;}     
        public string fullname {get;set;}
        public string email {get;set;}
        public string phone{get;set;}
        public string va_no{get;set;}
        public string password{get;set;}
        public string regid{get;set;}
        public bool deleted{get;set;}
        public bool login{get;set;}
        public DateTime ? loginTime{get;set;}
        public string token{get;set;}
        public DateTime ? createdAt { get; set; }
        public DateTime ? updatedAt{get;set;}

    }
}
