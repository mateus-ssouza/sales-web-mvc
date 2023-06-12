using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class DepartamentosController : Controller
    {
        public IActionResult Index()
        {
            List<Departamento> list = new List<Departamento>();
            list.Add(new Departamento
            {
                Id = 1,
                Nome = "Eletronics"
            });
            
            list.Add(new Departamento
            {
                Id = 1,
                Nome = "Fashion"
            });

            return View(list);
        }
    }
}
