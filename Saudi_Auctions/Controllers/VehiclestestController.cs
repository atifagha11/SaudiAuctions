using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Description;
using Saudi_Auctions.DAL;

namespace Saudi_Auctions.Controllers
{
    public class VehiclestestController : ApiController
    {
        private SaudiAuctionDBEntities db = new SaudiAuctionDBEntities();

        // GET: api/Vehiclestest
       
        public IQueryable<Vehicle> GetVehicles()
        {
            return db.Vehicles;
        }

        // GET: api/Vehiclestest/5
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult GetVehicle(decimal id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        // PUT: api/Vehiclestest/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehicle(decimal id, Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            db.Entry(vehicle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehiclestest
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult PostVehicle(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vehicle.Id }, vehicle);
        }

        // DELETE: api/Vehiclestest/5
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult DeleteVehicle(decimal id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            db.Vehicles.Remove(vehicle);
            db.SaveChanges();

            return Ok(vehicle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehicleExists(decimal id)
        {
            return db.Vehicles.Count(e => e.Id == id) > 0;
        }
    }
}