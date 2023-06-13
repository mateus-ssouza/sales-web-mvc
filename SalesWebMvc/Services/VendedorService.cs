using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class VendedorService
    {
        private readonly Contexto _contexto;

        public VendedorService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Vendedor>> FindAllAsync()
        {
            return await _contexto.Vendedores.ToListAsync();
        }

        public async Task InsertAsync(Vendedor obj)
        {
            _contexto.Add(obj);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Vendedor> FindByIdAsync(int id)
        {
            return await _contexto.Vendedores
                .Include(obj => obj.Departamento)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _contexto.Vendedores.FindAsync(id);
            _contexto.Vendedores.Remove(obj);
            await _contexto.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vendedor obj)
        {
            bool hasAny = await _contexto.Vendedores.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny) throw new NotFoundException("Id não encontrado");

            try
            {
                _contexto.Update(obj);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
