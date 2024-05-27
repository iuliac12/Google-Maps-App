using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoogleMap.Controllers
{
    public class HomeController : Controller
    {

        private Google_mapsEntities db = new Google_mapsEntities();

        // Endpoint care returnează lista de utilizatori și locațiile lor
        [HttpGet]
        public JsonResult GetAllUsers()
        {
            var users = db.Users.Select(u => new
            {
                UserName = u.User_Name,
                Latitude = u.Latitude,
                Longitude = u.Longitude
            }).ToList();

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveUserLocation(string userName, double latitude, double longitude)
        {
            try
            {
                // Căutăm dacă există deja un utilizator cu același nume în baza de date
                var existingUser = db.Users.FirstOrDefault(u => u.User_Name == userName);

                // Dacă utilizatorul există, actualizăm locația lui
                if (existingUser != null)
                {
                    existingUser.Latitude = latitude;
                    existingUser.Longitude = longitude;
                }
                // Dacă utilizatorul nu există, creăm unul nou
                else
                {
                    db.Users.Add(new User
                    {
                        User_Name = userName,
                        Latitude = latitude,
                        Longitude = longitude
                    });
                }

                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        public ActionResult Index()
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

        public ActionResult Map()
            
            {
                ViewBag.Message = "Your map page.";

                return View();
            }


        }
}