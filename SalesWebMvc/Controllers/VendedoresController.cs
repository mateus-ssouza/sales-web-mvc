using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System.Diagnostics;

namespace SalesWebMvc.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;

        public VendedoresController(VendedorService vendedorService, 
            DepartamentoService departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }

        public IActionResult Index()
        {
            var list = _vendedorService.FindAll();

            return View(list);
        }

        public IActionResult Create()
        {  
            var departamentos = _departamentoService.FindAll();
            var viewModel = new VendedorFormViewModel 
            { 
                Departamentos = departamentos
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendedor vendedor)
        {
            if(!ModelState.IsValid)
            {
                var departamentos = _departamentoService.FindAll();
                var viewModel = new VendedorFormViewModel
                {
                    Departamentos = departamentos
                };

                return View(viewModel);
            }

            _vendedorService.Insert(vendedor);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não reconhecido"});
            }

            var obj = _vendedorService.FindById(id.Value);
            
            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });

            return View(obj);   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _vendedorService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não reconhecido" });
            }

            var obj = _vendedorService.FindById(id.Value);

            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não reconhecido" });
            }

            var obj = _vendedorService.FindById(id.Value);

            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });

            List<Departamento> departamentos = _departamentoService.FindAll();
            VendedorFormViewModel viewMdoel = new VendedorFormViewModel
            {
                Vendedor = obj,
                Departamentos = departamentos
            };

            return View(viewMdoel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = _departamentoService.FindAll();
                var viewModel = new VendedorFormViewModel
                {
                    Departamentos = departamentos
                };

                return View(viewModel);
            }

            if (id != vendedor.Id) return RedirectToAction(nameof(Error), 
                new { message = "Id não correspondem" }); ;

            try
            {
                _vendedorService.Update(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }   
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
