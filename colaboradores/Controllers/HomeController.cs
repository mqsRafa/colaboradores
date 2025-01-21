using Microsoft.AspNetCore.Mvc;
using colaboradores.Data;
using colaboradores.Models;
using colaboradores.ViewModels;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace colaboradores.Controllers
{
    public class HomeController : Controller
    {
        private TesteContext _context;

        public HomeController(TesteContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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
