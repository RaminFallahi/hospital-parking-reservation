using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hospital_project4.Models
{
    public class Fee
    {
        [Key]
        public int FeeId { get; set; }
        public string FeeName { get; set; }
        public decimal Price { get; set; }

        //one fee blongs to one parking lot
        //one parking lot blongs to many fees
        [ForeignKey("Lot")]
        public int LotId { get; set; }
        public virtual Lot Lot  { get; set; }
    }

    //Data Transfer Object Method (DTO)

    //This method is used to transfer data from the database to the controller /and from the controller to the view
    //in the other word DTO is used to transfer data from the database to the view
    public class FeeDto
    {
        public int FeeId { get; set; }
        public string FeeName { get; set; }
        public decimal Price { get; set; }
        public int LotId { get; set; }
        public string LotName { get; set; }
    }
}