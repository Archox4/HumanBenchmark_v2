using HumanBenchmark_v2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Firebase.Auth;
using HumanBenchmark_v2.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol;
using NuGet.Common;

namespace HumanBenchmark_v2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        FirebaseAuthProvider auth;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyA-eoPydpcHMoMQuuwJtGaPSp_ySJhgPew"));
            ViewBag.AuthProvider = auth;
            
        }

        public IActionResult Index()
        {
            ViewBag.AuthProvider = auth;
            
            var token = HttpContext.Session.GetString("_UserToken");
            
            if (token != null)
            {
                ViewBag.tk = true;
                DB db = new DB();
                db.check_user();
                //Console.WriteLine("token: " + ViewBag.UID);

                return View();
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }

        public IActionResult Dashboard()
        {
            var token = HttpContext.Session.GetString("_UserToken");
            if (token != null)
            {
                ViewBag.tk = true;
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult SignIn()
        {
			var token = HttpContext.Session.GetString("_UserToken");

			return View();
        }
        public IActionResult ReactionTime()
        {
            var token = HttpContext.Session.GetString("_UserToken");
            if (token != null)
            {
                ViewBag.tk = true;
                return View();
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        public IActionResult NumberMemory()
        {
            var token = HttpContext.Session.GetString("_UserToken");
            if (token != null)
            {
                ViewBag.tk = true;
                return View();
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }

        public IActionResult WordsMemory()
        {
            var token = HttpContext.Session.GetString("_UserToken");
            if (token != null)
            {
                ViewBag.tk = true;
                return View();
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            //create the user
            await auth.CreateUserWithEmailAndPasswordAsync(userModel.Email, userModel.Password);
            //log in the new user
            var fbAuthLink = await auth
                            .SignInWithEmailAndPasswordAsync(userModel.Email, userModel.Password);
            string token = fbAuthLink.FirebaseToken;
            //saving the token in a session variable
            if (token != null)
            {
                HttpContext.Session.SetString("_UserToken", token);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserModel userModel)
        {
            //log in the user
            var fbAuthLink = await auth
                            .SignInWithEmailAndPasswordAsync(userModel.Email, userModel.Password);
            string token = fbAuthLink.FirebaseToken;

            //saving the token in a session variable
            if (token != null)
            {
                HttpContext.Session.SetString("_UserToken", token);
                Console.WriteLine("token: " + fbAuthLink.User.LocalId);
                HttpContext.Session.SetString("_UID", fbAuthLink.User.LocalId);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult LogOut()
        {
            ViewBag.tk = false;
            HttpContext.Session.Remove("_UserToken");
            HttpContext.Session.Remove("_UID");
            HttpContext.Session.Remove("_Username");
			return RedirectToAction("SignIn");
        }

        public IActionResult Settings()
        {
            var token = HttpContext.Session.GetString("_UserToken");
            if (token != null)
            {
                ViewBag.tk = true;
                DB db = new DB();
                string UID = HttpContext.Session.GetString("_UID");
                ViewBag.X = UID;
                if (UID != null)
                {
                    string x = db.get_username(ViewBag.X);
                    HttpContext.Session.SetString("_Username", x);
                    ViewBag.Username = x;
                }
                
                
                
                return View();
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }

		[HttpPost]
		public IActionResult SaveUsername(UserModel userModel)
		{
			DB db = new DB();
			if (userModel.Username.Length != 0)
			{
				if (db.username_state(userModel.Username) == true)
				{
					db.set_username(HttpContext.Session.GetString("_UID"), userModel.Username);

					return RedirectToAction("Index");
				}
				else
				{
					return View();
				}
			}
			else
			{
				return View();
			}

		}
	}
}