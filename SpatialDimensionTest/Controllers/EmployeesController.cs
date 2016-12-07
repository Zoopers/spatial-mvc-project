using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpatialDimensionTest.Models;

namespace SpatialDimensionTest.Controllers
{
    public class EmployeesController : Controller
    {
        DbContextCollection db = new DbContextCollection();

        public ActionResult Index()
        {
            if (Session["LoginName"] != null)
            {
                //returns list of all employees
                return View(db.Employees.ToList());
            }
            else
            {
                //redirect to the Login view found in the Home controller
                return RedirectToAction("Login", "Home", new { area = "" });
            }
        }

        public ActionResult PermissionDenied() {
            return View();
        }

        public ActionResult Search(string search)
        {
            
                //if no string is given, no records are loaded and the page remains empty.
                if (search == "")
                {
                    return RedirectToAction("Search");
                }
                else //Else, display any records that match the string
                {
                    return View(db.Employees.Where(x => x.EmpName.Contains(search)).ToList());
                }
            
        }

        public ActionResult Relationships(string id, string Sid)
        {

            if (Session["LoginName"] != null)
            {
                if (id == "" || id == null)
                {
                    //something broke so return an error message so that we can investigate
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    return View(db.Employees.Where(x => x.Superior.ToString().Contains(id)).ToList());
                }

            }
            else
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
        }

        //Employee details view
        public ActionResult Details(int id)
        {
            if (Session["LoginName"] != null)
            {
                //find the record for the employee that has been called
                //it should be impossible for this to be null
                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                db.Employees.Find(employee.Superior);
                return View(employee);
            }
            else
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
        }

        // // Access view for adding a new employee
        public ActionResult Create()
        {
            //Check if user is logged in and if the account has write privileges
            if (Session["LoginName"] != null && Session["PermissionLevel"].ToString() == "2")
            {
                return View();
            }
            else
            {
                return RedirectToAction("PermissionDenied");
            }
        }

        //Commit new employee record to database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpID,EmpName,Job,Superior")] Employee employee)
        {
            //Check if the user account has the correct privilege level to access this page
            //it should be impossible for this to be null
            if (Session["LoginName"] != null && Session["PermissionLevel"].ToString() == "2")
            {
                if (ModelState.IsValid)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(employee);
            }
            else
            {
                return RedirectToAction("PermissionDenied");

            }
        }

        // Access view for editing a specific employee
        public ActionResult Edit(int id)
        {
            //Check if the user account has the correct privilege level to access this page
            if (Session["LoginName"] != null && Session["PermissionLevel"].ToString() == "2")
            {
                //find the record that matches the id
                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }

            else
            {
                return RedirectToAction("PermissionDenied");
            }
        }

        //Post edited employee record to database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpID,EmpName,Job,Superior")] Employee employee)
        {

            if (Session["LoginName"] != null && Session["PermissionLevel"].ToString() == "2")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(employee);
            }

            else
            {
                return RedirectToAction("PermissionDenied");
            }
        }

        // Open Delete Employee view
        public ActionResult Delete(int id)
        {
            if (Session["LoginName"] != null && Session["PermissionLevel"].ToString() == "2")
            {   
                Employee employee = db.Employees.Find(id);
                return View(employee);
            }
            else
            {
                return RedirectToAction("PermissionDenied");
            }
        }

        // Commit deleted record to database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //delete button has been pressed, so delete the record and commit changes to the database
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
