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
    public class FeeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FeeData/ListFees
        [HttpGet]
        public IEnumerable<FeeDto> ListFees()
        {
            List<Fee> fees = db.Fees.Include(f => f.Lot).ToList();
            List<FeeDto> feeDtos = new List<FeeDto>();

            fees.ForEach(f => feeDtos.Add(new FeeDto()
            {
                FeeId = f.FeeId,
                FeeName = f.FeeName,
                Price = f.Price,
                LotId = f.Lot.LotId,
                LotName = f.Lot.LotName
            }));
            return feeDtos;
        }

        // GET: api/FeeData/FindFee/5
        [ResponseType(typeof(Fee))]
        [HttpGet]
        public IHttpActionResult FindFee(int id)
        {
            Fee Fee = db.Fees.Find(id);
            if (Fee == null)
            {
                return NotFound();
            }

            FeeDto FeeDto = new FeeDto()
            {
                FeeId = Fee.FeeId,
                FeeName = Fee.FeeName,
                Price = Fee.Price,
                LotId = Fee.Lot.LotId,
                LotName = Fee.Lot.LotName
            };

            return Ok(FeeDto);
        }

        // POST: api/FeeData/UpdateFee/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateFee(int id, Fee fee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fee.FeeId)
            {
                return BadRequest();
            }

            db.Entry(fee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeeExists(id))
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

        // POST: api/FeeData/AddFee
        [ResponseType(typeof(Fee))]
        [HttpPost]
        public IHttpActionResult AddFee(Fee fee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fees.Add(fee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fee.FeeId }, fee);
        }

        // POST: api/FeeData/5
        [ResponseType(typeof(Fee))]
        [HttpPost]
        public IHttpActionResult DeleteFee(int id)
        {
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return NotFound();
            }

            db.Fees.Remove(fee);
            db.SaveChanges();

            return Ok(fee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeeExists(int id)
        {
            return db.Fees.Count(e => e.FeeId == id) > 0;
        }
    }
}