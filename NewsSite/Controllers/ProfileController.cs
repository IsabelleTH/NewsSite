using Microsoft.AspNet.Identity;
using NewsSite.Data;
using NewsSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NewsSite.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Settings()
        {
            return View();
        }
        // GET: Profile
        public ActionResult EditProfilePicture()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfilePicture([Bind(Exclude = "UserPhoto")] ApplicationUser model)
        {
            var dbContext = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var id = User.Identity.GetUserId();

                //First, convert the photo into a byte array before saving it to DB
                byte[] imageFile = null;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase fileBase = Request.Files["UserPhoto"];

                    using (var binary = new BinaryReader(fileBase.InputStream))
                    {
                        imageFile = binary.ReadBytes(fileBase.ContentLength);
                    }
                }

                var user = dbContext.Users.Where(m => m.Id == id).FirstOrDefault();
                user.UserPhoto = imageFile;
                dbContext.SaveChanges();
                ViewBag.Message = "The profile picture has been changed";
                return View();
            }

            ViewBag.Error = "The user photo could not be changed";
            return View();
        }

        public ActionResult EditUserName()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserName(string userName)
        {
            var dbContext = new ApplicationDbContext();
            if(User.Identity.IsAuthenticated)
            {
                var id = User.Identity.GetUserId();
                var user = dbContext.Users.Where(m => m.Id == id).FirstOrDefault();
                user.UserName = userName;
                dbContext.SaveChanges();
                ViewBag.Message = "The user name was changed";
                return View();
            }
            ViewBag.Error = "The user name could not be changed";
            return View();
        }

    
            
    }
}
