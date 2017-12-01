using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maps.Models;

namespace Maps.Controllers {
  public class MapsController : Controller {
    // GET: Maps
    MapsDB db = new MapsDB();
    public ActionResult Index() {
      return View();
    }
  }
}