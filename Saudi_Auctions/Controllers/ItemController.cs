using Saudi_Auctions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Saudi_Auctions.Controllers
{
    public class ItemController : Controller
    {
       
        public ActionResult Index()
        {
            var vehicles = new VehicleRepository().GetAll();
            return View(vehicles);
         
        }
    }
}