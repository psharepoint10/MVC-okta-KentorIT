using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Ken.MVC.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            var identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
            return View(identity.Claims);
        }

        [Authorize]
        public ActionResult Index2()
        {
            var identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
            return View(identity.Claims);
        }
        [Authorize]
        public ActionResult Secure()
        {
            var identity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
            return View(identity.Claims);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}