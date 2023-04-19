using mi_taller_contenedores.DB.Model;

namespace mi_taller_contenedores.Servicios.API
{
    public interface IFacturaServices
    {
        Task<Factura> Get(int id, CancellationToken token);
        Task<IEnumerable<decimal>> GetMontosTotalesPaginado(int salto, int tamañoPagina, CancellationToken cancellation);
        Task<Factura> Insert(Factura factura);
        Task<Factura> Update(Factura factura);
        Task<(int, string)> UpdatePasajeros(int id, int pasajeros);
    }
}