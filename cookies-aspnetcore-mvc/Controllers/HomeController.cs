using cookies_aspnetcore_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cookies_aspnetcore_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHttpContextAccessor Accessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor _accessor)
        {
            _logger = logger;
            this.Accessor = _accessor;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCookie(string name)
        {
            if (name != null)
            {
                //Set the Expiry date of the Cookie.
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(30);

                //Create a Cookie with a suitable Key and add the Cookie to Browser.
                Response.Cookies.Append("Name", name, option);

            }
            TempData["Message"] = name != null ? name : "undefined";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ReadCookie()
        {
            //Fetch the Cookie value using its Key.
            string name = this.Accessor.HttpContext.Request.Cookies["Name"];

            TempData["Message"] = name != null ? name : "undefined";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteCookie()
        {
            //Delete the Cookie from Browser.
            Response.Cookies.Delete("Name");

            return RedirectToAction("Index");
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