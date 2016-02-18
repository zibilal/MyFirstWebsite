using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using MyFirstWebsite.CustomLibraries;
using MyFirstWebsite.DAL;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST
        [HttpPost]
        public ActionResult Login(Users model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new MainDbContext())
            {
                var getPassword = db.Users.Where(u => u.Email == model.Email).Select(u => u.Password);
                var materializedPassword = getPassword.ToList();
                var password = materializedPassword[0];
                var decryptedPassword = CustomDecrypt.Decrypt(password);

                if (model.Email != null && model.Password == decryptedPassword)
                {
                    var identity = new ClaimsIdentity(new []
                    {
                        new Claim(ClaimTypes.Name, "Xtian"),
                        new Claim(ClaimTypes.Email, "xtian@email.com"),
                        new Claim(ClaimTypes.Country, "Philippines"), 
                    }, "ApplicationCookie");
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Users model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    var encryptedPassword = CustomEncrypt.Encrypt(model.Password);
                    var user = db.Users.Create();
                    user.Email = model.Email;
                    user.Password = encryptedPassword;
                    user.Country = model.Country;
                    user.Name = model.Name;
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields have been");
            }
            return View();
        }
    }
}