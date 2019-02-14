using Saudi_Auctions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Saudi_Auctions.Controllers
{
   
    public class VehicleApiController : ApiController
    {
        [HttpPost]
       public IHttpActionResult SaveVehicle(Vehicle vehicle)
        {
            
            
            var getuser = new VehicleRepository().Add(vehicle);

            VehicleImage img = new VehicleImage();
            img.ImageName = "test";
            img.VechicleId = vehicle.Id;
            img.IsDeleted = false;
            
            vehicle.VehicleImages.Add(img);
           
            return Json(new { data = vehicle.Id, message = "Saved!", success = "false" });

        }
        



    }
}
