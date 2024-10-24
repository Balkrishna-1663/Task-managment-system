using Microsoft.AspNetCore.Mvc;

namespace Task_managment_system.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
