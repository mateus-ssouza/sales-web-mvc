using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

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

        public Vendedor FindById(int id)
        {
            return _contexto.Vendedores
                .Include(obj => obj.Departamento)
                .FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _contexto.Vendedores.Find(id);
            _contexto.Vendedores.Remove(obj);
            _contexto.SaveChanges();
        }
    }
}
