using Saudi_Auctions.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Saudi_Auctions.Controllers
{
    public class VehicleController : Controller
    {
        private SaudiAuctionDBEntities db = new SaudiAuctionDBEntities();

        public ActionResult AddVehicle()
        {
           
            return View();
        }
        public JsonResult SaveVehicleImages(string Id, string Images)
        {
            string res = "";
            string[] imag = Images.Split(',');
            foreach (var item in imag)
            {
                var n = new VehicleImage();
                n.ImageName = item;
                n.VechicleId = Convert.ToInt32(Id);
                var save = new VehicleImagesRepository().Add(n);
                if (save)
                {
                    res = "true";
                }
                else
                {
                    res = "false";
                }

            }
            return Json(res,JsonRequestBehavior.AllowGet);
          
        }
        public ActionResult Index()
        {

            var vehicles = new VehicleRepository().GetAll();
            return View(vehicles);
        }
        public ActionResult Upload()
        {
            string path = string.Empty;
            List<string> filePath = new List<string>();
            if (Request.Files != null)
                try
                {
                    var folder = Server.MapPath("~/Image/");
                    var folderexist = System.IO.Directory.Exists(folder);
                    if (!folderexist)
                    {
                        System.IO.Directory.CreateDirectory(folder);
                    }
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];

                        var fileName = Path.GetFileName(file.FileName);
                        var name = fileName.Split('.')[0];
                        var extension = fileName.Split('.')[1];
                        Random rand = new Random();
                        var r = rand.Next(1, 99999);


                        name = name + r;
                        fileName = name + "." + extension;
                        path = Path.Combine(Server.MapPath("/Image/"), fileName);
                        filePath.Add(fileName);
                        file.SaveAs(path);

                    }
                }
                catch (Exception)
                {

                }
            else
            {
                return Json("false");
            }
            return Json(filePath);

        }

        public IQueryable<Vehicle> GetVehicles()
        {
            return db.Vehicles;
        }
    }
}