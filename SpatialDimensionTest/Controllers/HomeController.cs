using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using SpatialDimensionTest.Models;

namespace SpatialDimensionTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //don't allow access to this view unless the user is logged in
            //TODO: Extend this to all pages and implement user permissions to bar some users from accessing pages that allow you to edit records

            //check whether the User is logged in
            if (Session["LoginName"] != null)
            {
                return View();
            }
            else {
                //user is not logged in so return to the login page. 
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(RegisteredUsers user)
        {
            using (DbContextCollection db = new DbContextCollection())
            {
                //create a variable to hold the records that matches the user input
                //value is NULL if no matching record is found
                var userA = db.RegisteredUsers.Where(x => x.LoginName == user.LoginName && x.Password == user.Password).FirstOrDefault();

                if (userA != null)
                {
                    //create session variables for the user
                    Session["LoginName"] = userA.LoginName.ToString();
                    Session["PermissionLevel"] = userA.PermissionLevel.ToString();
                    return RedirectToAction("Index");
                }
                else {
                    //user did not match a record in the database. Either the user does not exist or the credentials are incorrect
                    ModelState.AddModelError("","Username or password is incorrect");
                }
            }

            return View();
        }

    }
}