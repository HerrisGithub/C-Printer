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
    public class printerSetup
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int merchantId { get; set; }
        public bool defaults { get; set; }
        public string type { get; set; }
        [ForeignKey("merchantId")]
        [JsonIgnore]
        public virtual merchant merchant { get; set; }
    }
}
