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

        public async Task<List<RegistroVendas>> FindByDateAsync(DateTime? minData, 
            DateTime? maxData)
        {
            var result = from obj in _contexto.RegistroVendas select obj;

            if (minData.HasValue)
            {
                result = result.Where(x => x.Data >= minData.Value);
            }

            if (maxData.HasValue)
            {
                result = result.Where(x => x.Data <= maxData.Value);
            }

            return await result
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
        }
    }
}
