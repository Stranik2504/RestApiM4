using Microsoft.AspNetCore.Mvc;

namespace RestApiM4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
