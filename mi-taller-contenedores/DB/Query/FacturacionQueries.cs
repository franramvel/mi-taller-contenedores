using mi_taller_contenedores.DB.Model;
using mi_taller_contenedores.DB.Query.Interfaces;

namespace mi_taller_contenedores.DB.Query
{
    //Definicion de nuestros Queryes, el como se llaman, y sus parametros
    //Un record, es una clase inmutable
    //LO QUE DECLARAMOS EN EL CONSTRUCTOR SE COMPORTA COMO PROPIEDADES
    public record QueryGetFacturacion(int Id);
    public record QueryGetFacturacionByRazonSocial(string RazonSocial);
    public record QueryGetMontosTotalesPaginado(int Salto, int TamañoPagina);
                  //Eq. Nombre SP     //Eq. Parametros
    //Definición de la interfaz, Todos los queries, y lo que van a devolver
    public interface IFacturacionQueryHandler:
        IQueryHandler<QueryGetFacturacion,Factura>,//Esta interfaz se declara por cada query  que vayamos a usar
        IQueryHandler<QueryGetFacturacionByRazonSocial, IEnumerable<Factura>>,//Esta interfaz se declara por cada query  que vayamos a usar
        IQueryHandler<QueryGetMontosTotalesPaginado, IEnumerable<decimal>>
    {

    }

    //QueryObject
    public class FacturacionQueries: IFacturacionQueryHandler
    {
        private readonly MainDbContext _ctx;

        public FacturacionQueries(MainDbContext ctx)
        {
            _ctx = ctx;
        }

                     //Task<Loqueregresa> Handle(Loquenecesita,cancellation)
        public async Task<Factura> Handle(QueryGetFacturacion query, CancellationToken cancellation)
        {
            var factura = _ctx.TblFacturas.Where(factura => factura.Id == query.Id).FirstOrDefault();
            return factura;
        }

        public async Task<IEnumerable<Factura>> Handle(QueryGetFacturacionByRazonSocial query, CancellationToken cancellation)
        {
            var factura = _ctx.TblFacturas.Where(factura=>factura.RazonSocial.Contains(query.RazonSocial)).ToList();
            return factura;
        }

        public async Task<IEnumerable<decimal>> Handle(QueryGetMontosTotalesPaginado query, CancellationToken cancellation)
        {
            //List es el equivalente a ArrayList en java, o [] en python
            var montos = new List<decimal>();

            IQueryable<Factura> linqQuery =
             (from factura in _ctx.TblFacturas
              select new Factura
              {
                  Pasajeros = factura.Pasajeros,
                  MontoPorPasajero = factura.MontoPorPasajero
              });
            //El total va antes del skip y el take
            var totalDeRegistros = linqQuery.Count();

            linqQuery.Skip(query.Salto).Take(query.TamañoPagina);
            //Si se pone despues, ya no devuelve el total, devuelve el ya filtrado
            //var totalDeRegistros = query.Count(); ESTE NO VA AQUI

            var facturas = linqQuery.ToList();

            foreach (var factura in facturas)
            {
                var calculo = factura.Pasajeros * factura.MontoPorPasajero;
                montos.Add(calculo);
            }


            return montos;
        }
    }
}
