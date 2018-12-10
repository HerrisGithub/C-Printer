// using OkgoPrinter.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OkgoPrinter.Models
{
    public class transaction
    {
        public transaction(){
            this.listTransactionDetail = new HashSet<transactionDetail>();
        }
        [Key]
        public int id { get; set; }
        public int merchantId { get; set; }
        public int pickup { get; set; }
        public DateTime ?  pickupDate { get; set; }   
        public string tableNo {get; set;}     
        public string invoice {get;set;}
        public int userId {get;set;}
        public int discount {get;set;}
        public decimal totalBT{get;set;}
        public decimal serviceFee{get;set;}
        public decimal taxFee{get;set;}
        public decimal total{get;set;}
        public string status{get;set;}
        public string remark{get;set;}
        public int updatedBy{get;set;}
        public string payment {get;set;}
        public DateTime ? createdAt { get; set; }
        public DateTime ? updatedAt{get;set;}
        public ICollection<transactionDetail> listTransactionDetail {get;set;}
        [ForeignKey("userId")]
        public virtual user user {get;set;}
         [ForeignKey("merchantId")]
        public virtual merchant merchant {get;set;}
        

    }
}
