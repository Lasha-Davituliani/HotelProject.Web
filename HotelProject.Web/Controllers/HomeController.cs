using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelProject.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }       
       
    }
}
