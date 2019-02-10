using Saudi_Auctions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Saudi_Auctions.Controllers
{
    public class VehicleDelApiController : ApiController
    {
        [HttpPost]
        public IHttpActionResult DVehicle(Vehicle Vehicle)
        {
           
            Vehicle vch = new VehicleRepository().FirstOrDefault(c => c.Id == Vehicle.Id);
            new VehicleRepository().Delete(Vehicle.Id);

            return Ok();

        }
    }
}
