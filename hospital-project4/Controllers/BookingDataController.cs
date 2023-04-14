using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using hospital_project4.Models;
using System.Diagnostics;


namespace hospital_project4.Controllers
{
    public class BookingDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BookingData/ListBookings
        [HttpGet]
        public IEnumerable<BookingDto> ListBookings()
        {
            List<Booking> Bookings = db.Bookings.ToList();
            List<BookingDto> BookingDtos = new List<BookingDto>();

            Bookings.ForEach(b => BookingDtos.Add(new BookingDto()
            {
                BookingId = b.BookingId,
                BookingName = b.BookingName,
                Date = b.Date,
                FeeId = b.Fee.FeeId,
                Price = b.Fee.Price,
                LotName = b.Fee.Lot.LotName,
                FeeName = b.Fee.FeeName
            }));
            return BookingDtos;
        }

        // GET: api/BookingData/FindBooking/5
        [ResponseType(typeof(Booking))]
        [HttpGet]
        public IHttpActionResult FindBooking(int id)
        {
            Booking Booking = db.Bookings.Find(id);
            if (Booking == null)
            {
                return NotFound();
            }

            BookingDto BookingDto = new BookingDto()
            {
                BookingId = Booking.BookingId,
                BookingName = Booking.BookingName,
                Date = Booking.Date,
                FeeId = Booking.Fee.FeeId,
                Price = Booking.Fee.FeeId,
                LotName = Booking.Fee.Lot.LotName
            };

            return Ok(BookingDto);
        }

        // POST: api/BookingData/UpdateBooking/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateBooking(int id, BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            Fee fee = db.Fees.Find(bookingDto.FeeId);
            if (fee == null)
            {
                return BadRequest("Fee not found for FeeId: " + bookingDto.FeeId);
            }

            booking.BookingName = bookingDto.BookingName;
            booking.Date = bookingDto.Date;
            booking.Fee = fee;

            db.Entry(booking).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BookingData/AddBooking
        [ResponseType(typeof(Booking))]
        [HttpPost]
        public IHttpActionResult AddBooking(BookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state is not valid: " + ModelState);
            }

            Fee fee = db.Fees.Find(bookingDto.FeeId);
            if (fee == null)
            {
                return BadRequest("Fee not found for FeeId: " + bookingDto.FeeId);
            }

            Booking booking = new Booking()
            {
                BookingName = bookingDto.BookingName,
                Date = bookingDto.Date,
                Fee = fee
            };

            db.Bookings.Add(booking);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = booking.BookingId }, booking);
        }

        // POST: api/BookingData/deleteBooking/5
        [ResponseType(typeof(Booking))]
        [HttpDelete]
        public IHttpActionResult DeleteBooking(int id)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            db.Bookings.Remove(booking);
            db.SaveChanges();

            return Ok(booking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookingExists(int id)
        {
            return db.Bookings.Count(e => e.BookingId == id) > 0;
        }
    }
}