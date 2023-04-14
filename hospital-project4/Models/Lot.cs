using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hospital_project4.Models
{
    public class Lot
    {
        [Key]
        public int LotId { get; set; }
        public string LotName { get; set; }
    }
}