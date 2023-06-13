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

        public async Task<IActionResult> Index()
        {
            var list = await _vendedorService.FindAllAsync();

            return View(list);
        }

        public async Task<IActionResult> Create()
        {  
            var departamentos = await _departamentoService.FindAllAsync();
            var viewModel = new VendedorFormViewModel 
            { 
                Departamentos = departamentos
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            // TODO: Verificar porque não ta funcionando corretamente
            /*if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormViewModel
                {
                    Vendedor = vendedor,
                    Departamentos = departamentos
                };

                return View(viewModel);
            }*/

            await _vendedorService.InsertAsync(vendedor);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), 
                    new { message = "Id não reconhecido"});
            }

            var obj = await _vendedorService.FindByIdAsync(id.Value);
            
            if (obj == null) return RedirectToAction(nameof(Error), 
                new { message = "Id não encontrado" });

            return View(obj);   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _vendedorService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), 
                    new { message = "Id não reconhecido" });
            }

            var obj = await _vendedorService.FindByIdAsync(id.Value);

            if (obj == null) return RedirectToAction(nameof(Error), 
                new { message = "Id não encontrado" });

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), 
                    new { message = "Id não reconhecido" });
            }

            var obj = await _vendedorService.FindByIdAsync(id.Value);

            if (obj == null) return RedirectToAction(nameof(Error), 
                new { message = "Id não encontrado" });

            List<Departamento> departamentos = 
                await _departamentoService.FindAllAsync();

            VendedorFormViewModel viewMdoel = new VendedorFormViewModel
            {
                Vendedor = obj,
                Departamentos = departamentos
            };

            return View(viewMdoel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            // TODO: Verificar porque não ta funcionando corretamente
            /*if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormViewModel
                {
                    Vendedor = vendedor,
                    Departamentos = departamentos
                };

                return View(viewModel);
            }*/

            if (id != vendedor.Id) return RedirectToAction(nameof(Error), 
                new { message = "Id não correspondem" }); ;

            try
            {
                await _vendedorService.UpdateAsync(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), 
                    new { message = e.Message });
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
