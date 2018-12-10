using OkgoPrinter.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OkgoPrinter.Models
{
    public class merchant
    {
        // public merchant(){
        // }
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string creator { get; set; }
        public bool cashier { get; set; }
        public int discount {get;set;}
        public int serviceTax {get;set;}
        public int tax {get;set;}
        public DateTime ? createdAt { get; set; }
        public DateTime ? updatedAt { get; set; }
        // public ICollection<listing> listListing { set; get; }

    }
}
