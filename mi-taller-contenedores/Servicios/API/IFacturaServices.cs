using mi_taller_contenedores.DB.Model;

namespace mi_taller_contenedores.Servicios.API
{
    public interface IFacturaServices
    {
        Task<Factura> Get(int id, CancellationToken token);
        Task<IEnumerable<decimal>> GetMontosTotalesPaginado(int salto, int tamañoPagina, CancellationToken cancellation);
        Factura Insert(Factura factura);
    }
}