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

        public List<Departamento> FindAll()
        {
            return _contexto.Departamento.OrderBy(d => d.Nome).ToList();
        }
    }
}
