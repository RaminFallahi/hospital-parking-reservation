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

namespace hospital_project4.Controllers
{
    public class LotDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/LotData/ListLots
        [HttpGet]
        public IEnumerable<Lot> ListLots()
        {
            List<Lot> Lots = db.Lots.ToList();
            List<Lot> LotDtos = new List<Lot>();

            Lots.ForEach(l => LotDtos.Add(new Lot()
            {
                LotId = l.LotId,
                LotName = l.LotName,
            }));
            return LotDtos;
        }

        // GET: api/LotData/FindLot/5
        [ResponseType(typeof(Lot))]
        [HttpGet]
        public IHttpActionResult FindLot(int id)
        {
            Lot lot = db.Lots.Find(id);
            if (lot == null)
            {
                return NotFound();
            }
            Lot LotDto = new Lot()
            {
                LotId = lot.LotId,
                LotName = lot.LotName,
            };
            return Ok(LotDto);
        }

        // POST: api/LotData/UpdateLot/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateLot(int id, Lot lot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lot.LotId)
            {
                return BadRequest();
            }

            db.Entry(lot).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LotExists(id))
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

        // POST: api/LotData/AddLot
        [ResponseType(typeof(Lot))]
        public IHttpActionResult AddLot(Lot lot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lots.Add(lot);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lot.LotId }, lot);
        }

        // DELETE: api/LotData/5
        [ResponseType(typeof(Lot))]
        [HttpPost]
        public IHttpActionResult DeleteLot(int id)
        {
            Lot lot = db.Lots.Find(id);
            if (lot == null)
            {
                return NotFound();
            }

            db.Lots.Remove(lot);
            db.SaveChanges();

            return Ok(lot);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LotExists(int id)
        {
            return db.Lots.Count(e => e.LotId == id) > 0;
        }
    }
}