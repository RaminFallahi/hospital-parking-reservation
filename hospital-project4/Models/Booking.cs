using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hospital_project4.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public string BookingName { get; set; }
        public DateTime Date { get; set; }

        //one booking blongs to one fee
        //one booking fee blongs to many booking
        [ForeignKey("Fee")]
        public int FeeId { get; set; }
        public virtual Fee Fee { get; set; }
    }
    //Data Transfer Object Method (DTO)

    //This method is used to transfer data from the database to the controller /and from the controller to the view
    //in the other word DTO is used to transfer data from the database to the view
    public class BookingDto
    {
        public int BookingId { get; set; }
        public string BookingName { get; set; }
        public DateTime Date { get; set; }
        public int FeeId { get; set; }
        public string FeeName { get; set; }
        public decimal Price { get; set; }
        public string LotName { get; set; }
    }
}