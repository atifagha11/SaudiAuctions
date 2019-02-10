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
using Saudi_Auctions.DAL;

namespace Saudi_Auctions.Controllers
{
    public class LookupsController : ApiController
    {
        private SaudiAuctionDBEntities db = new SaudiAuctionDBEntities();

        // GET: api/Lookups
        public IQueryable<Lookup> GetLookups()
        {
            return db.Lookups;
        }

        // GET: api/Lookups/5
        [ResponseType(typeof(Lookup))]
        public IHttpActionResult GetLookup(decimal id)
        {
            Lookup lookup = db.Lookups.Find(id);
            if (lookup == null)
            {
                return NotFound();
            }

            return Ok(lookup);
        }

        // PUT: api/Lookups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLookup(decimal id, Lookup lookup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lookup.LookupId)
            {
                return BadRequest();
            }

            db.Entry(lookup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LookupExists(id))
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

        // POST: api/Lookups
        [ResponseType(typeof(Lookup))]
        public IHttpActionResult PostLookup(Lookup lookup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lookups.Add(lookup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lookup.LookupId }, lookup);
        }

        // DELETE: api/Lookups/5
        [ResponseType(typeof(Lookup))]
        public IHttpActionResult DeleteLookup(decimal id)
        {
            Lookup lookup = db.Lookups.Find(id);
            if (lookup == null)
            {
                return NotFound();
            }

            db.Lookups.Remove(lookup);
            db.SaveChanges();

            return Ok(lookup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LookupExists(decimal id)
        {
            return db.Lookups.Count(e => e.LookupId == id) > 0;
        }
    }
}