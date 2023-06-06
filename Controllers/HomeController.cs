using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tourismpk.Models;

namespace Tourismpk.Controllers
{
    public class HomeController : Controller
    {
        private TourismpkEntities db = new TourismpkEntities();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Booking(int? id, int? rn)

        {
            if (Session["TID"] == null)
            {
                return RedirectToAction("Login", "Tourists");
            }

            var p = db.Packages.Where(x => x.Pkg_Id == id).FirstOrDefault();
            Session["Rprice"] = p.Pkg_Price;

             Session["id"] = id;
            Session["rn"] = rn;

            return View();
        }
        public ActionResult BookedAppointment(DateTime? FDate, DateTime? TDate)
        {


            Session["Fdate"] = FDate;
            Session["Tdate"] = TDate;

            if (FDate == null || TDate == null)
            {

                return Content("<script language='javascript' type='text/javascript'>alert('Enter CheckIn and CheckOut!');</script>");

            }
            if (FDate < DateTime.Now)
            {

                return Content("<script language='javascript' type='text/javascript'>alert('fromDate cannot smaller than current date');</script>");

            }
            if (TDate < FDate)
            {

                return Content("<script language='javascript' type='text/javascript'>alert('ToDate Cannot be smaller than From Date');</script>");

            }
            int datif = ((TimeSpan)(TDate - FDate)).Days;

            if (datif > 7)
            {

                return Content("<script language='javascript' type='text/javascript'>alert('you cannot book room more than 7 days');</script>");

            }


            var idd = (int)Session["id"];

            var t = db.bookings.Where(x => (x.room_fid == idd) && ((x.booking_from <= FDate && x.booking_to >= TDate) || (x.booking_from <= FDate && x.booking_to >= FDate) || (x.booking_from <= TDate && x.booking_to >= TDate))).Select(x => x.room_fid).ToList();

            if (t.Count > 0)
            {
                var n = db.bookings.ToList();
                n = db.bookings.Where(y => t.Contains(y.room_fid)).ToList();
                //return RedirectToAction("index", "Home");
                return View(n.ToList());
            }
            else
            {


                //return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=ranazubair321@gmail.com&item_name=Tourismpk&return=https://localhost:44394/Home/OrderBooked&amount=" + double.Parse(Session["Rprice"].ToString()) / 220);


              


                return RedirectToAction("Confirm", "Home");

            }


        }

        public ActionResult OrderBooked()
        {

            var idd = (int)Session["id"];
            booking a = new booking();



            a.room_fid = idd;
            a.room_no = (int)Session["rn"]; ;


            a.booking_from = (DateTime)Session["Fdate"];
            a.booking_to = (DateTime)Session["Tdate"];






            db.bookings.Add(a);
            db.SaveChanges();

          
            return View(a);
            
        }
        public ActionResult proceedtopay()
        {

            return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=ranazubair321@gmail.com&item_name=FoodFun&return=https://localhost:44394/Home/OrderBooked&amount=" + double.Parse(Session["Rprice"].ToString()) / 220);


        }
        public ActionResult Confirm()
        {
          
            return View();
        }
        public ActionResult HomeVersion1()
        {
            return View();
        }

        public ActionResult HomeVersion2()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }

        public ActionResult Hotels(int? id)
        {
            PackageModel p = new PackageModel();
            p.cat = db.Categories.ToList();
            if(id==null)
            {
                p.pkg = db.Packages.ToList();
            }
            else
            {
                p.pkg = db.Packages.Where(z => z.Cat_Fid == id).ToList();
            }

            return View(p);
        }

        public ActionResult SearchHotels()
        {
            return View();
        }

        public ActionResult BookingHotels()
        {
            return View();
        }

        public ActionResult HotelReservation()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult RightBlog()
        {
            return View();
        }

        public ActionResult LeftBlog()
        {
            return View();
        }

        public ActionResult RightPost()
        {
            return View();
        }

        public ActionResult LeftPost()
        {
            return View();
        }

        public ActionResult FullPost()
        {
            return View();
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