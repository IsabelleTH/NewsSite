using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NewsSite.Data;
using NewsSite.Models;
using NewsSite.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NewsSite.Controllers
{
    public class AccountController : Controller
    {

        //Database injection
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public AccountController()
        {

        }
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }



        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        { 
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Apis");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt");
                    return View(model);

            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Exclude ="UserPhoto")] RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {

            //First, convert the chosen photo to byte array before saving it to DB
            byte[] imageData = null;
            if(Request.Files.Count > 0)
            {
                HttpPostedFileBase fileBase = Request.Files["UserPhoto"];

                using (var binary = new BinaryReader(fileBase.InputStream))
                {
                    imageData = binary.ReadBytes(fileBase.ContentLength);
                }
            }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                user.UserPhoto = imageData;
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Index", "Apis");
                }

            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();

                if(userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Content/Images/noImage.jpg");
                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader reader = new BinaryReader(stream);
                    imageData = reader.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");
                }

                var dbContxt = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var user = dbContxt.Users.Where(x => x.Id == userId).FirstOrDefault();

                return new FileContentResult(user.UserPhoto, "image/jpeg");
            } else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Content/Images/noImage.jpg");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                imageData = reader.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");
            }
        }

        //GET
        [Authorize]
        public ActionResult UserFavoriteCategory()
        {
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var model = db.Categories.ToList();
            return View(model);
        }

        [HttpPost, Authorize]
        public ActionResult UserFavoriteCategory(int categoryId)
        {
            //1. Get the user id 
            var userId = User.Identity.GetUserId();
            //2. Call the database
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            //3. Get the user from the db
            var user = db.Users.FirstOrDefault(m => m.Id == userId);
            //4. Set the incoming parameter categoryId to the category in users table
            user.CategoryId = categoryId;
            db.SaveChanges();
            //Implement redirection to the category headlines here if we would like to

            return RedirectToAction("Index", "Apis");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var dbContext = new ApplicationDbContext();

            var userId = User.Identity.GetUserId();
            var result = await UserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);

            if(result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(userId);
                if(user != null)
                {
                    await SignInManager.SignInAsync(user, false, false);
                }

                ViewBag.Success = "The password has been updated";
                return View();
            }
            ViewBag.Error = "The password was incorrect";
            return View();
        }

        //Log out

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
    }
}