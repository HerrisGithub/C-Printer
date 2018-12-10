using OkgoPrinter.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OkgoPrinter.Models
{
    public class subcategory
    {

        public subcategory(){
            this.ListListing = new HashSet<listing>();
        }
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string menu { get; set; }
        public ICollection<listing> ListListing {get;set;}
        public DateTime ? createdAt { get; set; }
        public DateTime ? updatedAt { get; set; }

    }
}
