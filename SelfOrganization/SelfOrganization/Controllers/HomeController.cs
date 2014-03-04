using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfOrganization.Models;
using WebMatrix.WebData;

namespace SelfOrganization.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db = new UsersContext();

        public ActionResult Index()
        {
            ViewBag.ProfileId = new SelectList(db.UserProfiles, "UserId", "UserName");

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }

    
}
