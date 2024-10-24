using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task_managment_system.Data;
using Task_managment_system.Models;
using Task_managment_system.Models.DTO;

namespace Task_managment_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDb context;

        public HomeController(ILogger<HomeController> logger, AppDb context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            var data = context.Tasks.ToList();
            return View(data);
        }

        public IActionResult Privacy()
        {
            var data = context.Categories.ToList();
            return View(data);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
