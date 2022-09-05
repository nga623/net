using Microsoft.AspNetCore.Mvc;
using Route.Models;
using System;
using System.Diagnostics;

namespace Route.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    //public class NoZeroesController : ControllerBase
    //{
    //    [HttpGet("{id:noZeroes}")]
    //    public IActionResult Get(string id) =>
    //        Content(id);
    //}

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            
               _logger = logger;
        }

        
        public IActionResult Index()
        {
            Console.WriteLine(123);
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