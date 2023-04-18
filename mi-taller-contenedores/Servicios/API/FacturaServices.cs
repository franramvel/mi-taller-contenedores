using mi_taller_contenedores.DB;
using mi_taller_contenedores.DB.Model;
using mi_taller_contenedores.DB.Query;
using mi_taller_contenedores.DB.Query.Interfaces;
using mi_taller_contenedores.Servicios.Genericos;

namespace mi_taller_contenedores.Servicios.API
{
    public class FacturaServices : IFacturaServices
    {
        private readonly MainDbContext _ctx;
        private readonly IFileManagementService _fileManagementService;
        private readonly IQueryDispatcher _qdispatcher;

        public FacturaServices(MainDbContext ctx, IFileManagementService fileManagementService,
            IQueryDispatcher _qdispatcher)
        {
            _ctx = ctx;
            _fileManagementService = fileManagementService;
            this._qdispatcher = _qdispatcher;
        }

        public async Task<Factura> Get(int id,CancellationToken token)
        {
            //se hace logica de negocios si es necesaria antes o despues del QO
            var query = new QueryGetFacturacion(id);
            var result = await _qdispatcher.Dispatch<QueryGetFacturacion, Factura>
                        (query, token);
            return result;
        }
        public Factura Insert(Factura factura)
        {
            //LOGICA DE NEGOCIOS
            //Logica para timbrado al PAC
            //Simulacion de llamada
            var uuid = Guid.NewGuid().ToString();
            factura.UUID = uuid;

            //Agregar archivo xml a una carpeta local
            var pathGuardado = _fileManagementService.SaveFile("folder/archivo.txt", "asdrthrehh54654654ty54gh45");
            factura.PathFileFactura = pathGuardado;

            //FIN LOGICA DE NEGOCIOS
            //Cuando llega la factura, aún no esta en la db
            _ctx.TblFacturas.Add(factura);
            //Si no llamamos al metodo save changes, la base de datos no se actualiza
            _ctx.SaveChanges();
            //EF lleva un tracking

            return factura;
        }

        public async Task<IEnumerable<decimal>> GetMontosTotalesPaginado(int salto, int tamañoPagina,CancellationToken cancellation)
        {
            //List es el equivalente a ArrayList en java, o [] en python
            //se hace logica de negocios si es necesaria antes o despues del QO
            var query = new QueryGetMontosTotalesPaginado(salto,tamañoPagina);
            var result = await _qdispatcher.Dispatch<QueryGetMontosTotalesPaginado, IEnumerable<decimal>>
                        (query, cancellation);
            return result;
        }
    }
}
