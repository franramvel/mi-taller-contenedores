using mi_taller_contenedores.DB;
using mi_taller_contenedores.DB.Model;
using mi_taller_contenedores.Servicios.Genericos;

namespace mi_taller_contenedores.Servicios.API
{
    public class FacturaServices : IFacturaServices
    {
        private readonly MainDbContext _ctx;
        private readonly IFileManagementService _fileManagementService;

        public FacturaServices(MainDbContext ctx, IFileManagementService fileManagementService)
        {
            _ctx = ctx;
            _fileManagementService = fileManagementService;
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

        public IEnumerable<decimal> GetMontosTotalesPaginadoIQueryable(int salto, int tamañoPagina)
        {
            //List es el equivalente a ArrayList en java, o [] en python
            var montos = new List<decimal>();

            IQueryable<Factura> query =
             (from factura in _ctx.TblFacturas
              select new Factura
              {
                  Pasajeros = factura.Pasajeros,
                  MontoPorPasajero = factura.MontoPorPasajero
              });
            //El total va antes del skip y el take
            var totalDeRegistros = query.Count();

            query.Skip(salto).Take(tamañoPagina);
            //Si se pone despues, ya no devuelve el total, devuelve el ya filtrado
            //var totalDeRegistros = query.Count(); ESTE NO VA AQUI

            var facturas = query.ToList();

            foreach (var factura in facturas)
            {
                var calculo = factura.Pasajeros * factura.MontoPorPasajero;
                montos.Add(calculo);
            }


            return montos;
        }
    }
}
