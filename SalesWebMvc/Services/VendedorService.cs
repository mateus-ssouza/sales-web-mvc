using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class VendedorService
    {
        private readonly Contexto _contexto;

        public VendedorService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public List<Vendedor> FindAll()
        {
            return _contexto.Vendedores.ToList();
        }

        public void Insert(Vendedor obj)
        {
            _contexto.Add(obj);
            _contexto.SaveChanges();
        }
    }
}
