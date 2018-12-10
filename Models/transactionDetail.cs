using OkgoPrinter.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OkgoPrinter.Models
{
    public class transactionDetail
    {
        [Key]
        public int id { get; set; }
        public int transactionId { get; set; }
        public int listingId { get; set; }
        public string category { get; set; }   
        public string lname {get; set;}     
        public int qty {get;set;}
        public decimal price {get;set;}
        public decimal subtotal{get;set;}
        public decimal discount{get;set;}
        public string remark{get;set;}
        public int updatedBy{get;set;}
        public string status {get;set;}
        public DateTime ? createdAt { get; set; }
        public DateTime ? updatedAt{get;set;}

        [ForeignKey("updatedBy")]
        public virtual user user {get;set;}
         [ForeignKey("listingId")]
        public virtual listing listing {get;set;}
        [JsonIgnore]
        [ForeignKey("transactionId")]
        public virtual transaction transaction{get;set;}
        

    }
}
