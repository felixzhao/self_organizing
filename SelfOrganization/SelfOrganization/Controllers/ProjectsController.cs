using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SelfOrganization.Models;
using WebMatrix.WebData;

namespace SelfOrganization.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectContext db = new ProjectContext();
        private UsersContext user_db = new UsersContext();

        //
        // GET: /Projects/

        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        //
        // GET: /Projects/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Projects/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                //project.CreatorID = WebSecurity.CurrentUserId;//Convert.ToInt32(Membership.GetUser().ProviderUserKey);
                db.Projects.Add(project);
                db.SaveChanges();

                ViewBag.UserList = new SelectList(user_db.UserProfiles.ToList(),"UserId","UserName");

                return RedirectToAction("Index");
            }

            return View(project);
        }

        
    }
}
