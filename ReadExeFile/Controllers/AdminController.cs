using Microsoft.AspNetCore.Mvc;

namespace ReadExeFile.Controllers
{

    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
