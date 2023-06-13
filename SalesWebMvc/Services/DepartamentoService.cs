using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class DepartamentoService
    {
        private readonly Contexto _contexto;

        public DepartamentoService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Departamento>> FindAllAsync()
        {
            return await _contexto.Departamento
                .OrderBy(d => d.Nome)
                .ToListAsync();
        }
    }
}
