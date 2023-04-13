using mi_taller_contenedores.DB.Model;

namespace mi_taller_contenedores.Servicios.API
{
    public interface IFacturaServices
    {
        IEnumerable<decimal> GetMontosTotalesPaginadoIQueryable(int salto, int tamañoPagina);
        Factura Insert(Factura factura);
    }
}