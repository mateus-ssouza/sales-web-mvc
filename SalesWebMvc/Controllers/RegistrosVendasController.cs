using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvc.Controllers
{
    public class RegistrosVendasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleSearch()
        {
            return View();
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
