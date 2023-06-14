using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class RegistroVendasService
    {
        private readonly Contexto _contexto;

        public RegistroVendasService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<RegistroVendas>> FindByDateAsync(DateTime? minDate, 
            DateTime? maxDate)
        {
            var result = from obj in _contexto.RegistroVendas select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);
            }

            return await result
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
        }

    }
}
