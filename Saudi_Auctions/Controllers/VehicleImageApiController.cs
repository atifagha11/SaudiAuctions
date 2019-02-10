using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Saudi_Auctions.DAL;
namespace Saudi_Auctions.Controllers
{
    public class VehicleImageApiController : ApiController
    {
        [HttpPost]
        public IHttpActionResult SaveVahicleImages(string Id,string Images)
        {
            string[] imag = Images.Split(',');
            foreach(var item in imag)
            {
                 var n=new VehicleImage();
                n.ImageName = item;
                n.VechicleId = Convert.ToInt32(Id);
                var save = new VehicleImagesRepository().Add(n);
                if(save)
                {
                    return Json(new { data = "null", message = "saved", success = "false" });
                }
                else
                {
                    return Json(new { data = "null", message = "NotSaved", success = "false" });
                }
                
            }
            return Ok();
        }
    }

}
