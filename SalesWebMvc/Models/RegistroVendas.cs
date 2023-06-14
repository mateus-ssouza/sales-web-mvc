using SalesWebMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class RegistroVendas
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public StatusVenda Status { get; set; }

        public Vendedor Vendedor { get; set; }

        public RegistroVendas()
        {
        }

        public RegistroVendas(int id, DateTime data, double valor, StatusVenda status, Vendedor vendedor)
        {
            Id = id;
            Data = data;
            Valor = valor;
            Status = status;
            Vendedor = vendedor;
        }
    }
}
