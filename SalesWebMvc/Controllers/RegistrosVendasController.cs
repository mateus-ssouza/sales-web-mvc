using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class RegistrosVendasController : Controller
    {
        private readonly RegistroVendasService _registroVendasService;

        public RegistrosVendasController(RegistroVendasService registroVendasService)
        {
            _registroVendasService = registroVendasService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate,
            DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _registroVendasService.FindByDateAsync(minDate, maxDate);

            return View(result);
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
