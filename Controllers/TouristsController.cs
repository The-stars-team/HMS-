using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tourismpk.Models;

namespace Tourismpk.Controllers
{
    public class TouristsController : Controller
    {
        private TourismpkEntities db = new TourismpkEntities();

        // GET: Tourists
        public ActionResult LogIn()
        {
            return View(db.Tourists.ToList());
        }

        [HttpPost]
        public ActionResult LogIn(Tourist t)
        {
           int res = db.Tourists.Where(x => x.Tourist_Email == t.Tourist_Email && x.Tourist_Password == t.Tourist_Password).Count();

            if(res > 0)
            {
                Session["TID"] = res;
                return RedirectToAction("Hotels", "Home");
            }
            else
            {
                Response.Write("<script>alert('Invalid UserName or Password');</script>");
                return View();
            }
        }

        // GET: Tourists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tourist tourist = db.Tourists.Find(id);
            if (tourist == null)
            {
                return HttpNotFound();
            }
            return View(tourist);
        }

        // GET: Tourists/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Tourists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "Tourist_Id,Tourist_FirstName,Tourist_LastName,Tourist_Email,Tourist_Password,Tourist_Contact,Tourist_Address")] Tourist tourist)
        {
            if (ModelState.IsValid)
            {
                db.Tourists.Add(tourist);
                db.SaveChanges();
                return RedirectToAction("LogIn", "Tourists");
            }

            return View(tourist);
        }

        // GET: Tourists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tourist tourist = db.Tourists.Find(id);
            if (tourist == null)
            {
                return HttpNotFound();
            }
            return View(tourist);
        }

        // POST: Tourists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Tourist_Id,Tourist_FirstName,Tourist_LastName,Tourist_Email,Tourist_Password,Tourist_Contact,Tourist_Address")] Tourist tourist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tourist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tourist);
        }

        // GET: Tourists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tourist tourist = db.Tourists.Find(id);
            if (tourist == null)
            {
                return HttpNotFound();
            }
            return View(tourist);
        }

        // POST: Tourists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tourist tourist = db.Tourists.Find(id);
            db.Tourists.Remove(tourist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
