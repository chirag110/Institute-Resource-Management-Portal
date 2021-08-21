using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EmptyProject.Models;

namespace EmptyProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
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

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.Flag = 0;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // extra field validation
            ApplicationDbContext db = new ApplicationDbContext();
            var res = db.user_data.Where(m => m.User_Email == model.Email).FirstOrDefault();
            if (model.Inst_Code != res.Inst_Code)
            {
                ModelState.AddModelError(model.Inst_Code, "Pa");
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    if ((int)res.RegisterAs != 2)
                    {
                        ViewBag.Flag = 1;
                    }
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            // dropdown for institute name
            ApplicationDbContext db = new ApplicationDbContext();
            ViewBag.Schools = new SelectList(db.institutes.ToList(), "Inst_Code", "Inst_Name");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Inst_Code = model.Inst_Code,
                    Fname = model.Fname,
                    Lname = model.Lname,
                    Mobile = model.Mobile,
                    RegisterAs = model.RegisterAs
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // --> start filling user details

                    ApplicationDbContext db = new ApplicationDbContext();
                    User std = new User();
                    std.Fname = model.Fname;
                    std.Inst_Code = model.Inst_Code;
                    std.Lname = model.Lname;
                    std.RegisterAs = model.RegisterAs;
                    std.Mobile = model.Mobile;
                    std.User_Email = model.Email;
                    std.Passw = model.Password;

                    // inst name, address
                    var res = db.institutes.Where(m => m.Inst_Code == model.Inst_Code).FirstOrDefault();
                    std.Inst_Name = res.Inst_Name;
                    std.Inst_Address = res.Inst_Address;
                    db.user_data.Add(std);
                    db.SaveChanges();

                    // <-- end filling user details

                    if ((int)model.RegisterAs == 0)
                    {
                        UserManager.AddToRole(user.Id, "admin");
                        UserManager.AddToRole(user.Id, "teacher");
                        UserManager.AddToRole(user.Id, "student");
                    }
                    if ((int)model.RegisterAs == 1)
                    {
                        UserManager.AddToRole(user.Id, "teacher");
                        UserManager.AddToRole(user.Id, "student");
                    }
                    if ((int)model.RegisterAs == 2)
                    {
                        UserManager.AddToRole(user.Id, "student");
                    }
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        #region Helpers
        private const string XsrfKey = "XsrfId";
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}