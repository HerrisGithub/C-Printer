using OkgoPrinter.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OkgoPrinter.Models
{
    public class listing
    {

       
        [Key]
        public int id { get; set; }
        public int merchantId { get; set; }
        public int merchantSpecificId { get; set; }
        public string category { get; set; }
        public int subcategoryId { get; set; }
        public string image{get;set;}
        public DateTime ? expired{get;set;}
        public string name{get;set;}
        public string ml{get;set;}
        public string detail{get;set;}
        public int iQty{get;set;}
        public int qty{get;set;}
        public decimal price{get;set;}
        public string remark{get;set;}
        public DateTime ? createdAt{get;set;}
        public DateTime ? updatedAt{get;set;}
        [ForeignKey("merchantId")]
         public virtual merchant merchant { get; set; }
        [ForeignKey("subcategoryId")]
         public virtual subcategory subcategory { get; set; }
    }
}
