using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Diagnostics;
using TimePass.DBData;
using TimePass.Models;

namespace TimePass.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        TimePassContext contex= new TimePassContext();
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
        {
            UserLogin userLogin = new UserLogin();
            var sActive = contex.Paras.FirstOrDefault();
            if (sActive != null)
            {
               

                userLogin.Active = sActive.Serveractive;
            }
            string key = "UserID";
            var cookieValue = Request.Cookies[key];
            if (cookieValue != null)
            {
                return RedirectToAction("Index", "User");
            }

            return View(userLogin);
		}
        [HttpPost]
        public   IActionResult Index(UserLogin Lg)
        {


            
            {
                var status = contex.Registrations.Where(m => m.Username == Lg.Username && m.Password == Lg.Password).FirstOrDefault();
                if (status == null)
                {
                    ViewBag.Status = 0;
                }
                else
                {

                    var LDetails = new UserLogin
                    {
                         Userid=status.UserId,
                        Username = status.Username,
                        Password=status.Password
                    };

                    //HttpContext.Session.SetInt32("AccountID", LDetails.Id);
                    //HttpContent.Session.SetString("Username",LDetails.Username);
                    string key = "UserID";
                    string value = LDetails.Userid.ToString();
                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(5)
                    };
                    Response.Cookies.Append(key, value, options);
                    return RedirectToAction("Index", "User");
                }
               
            }

            return View();
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
