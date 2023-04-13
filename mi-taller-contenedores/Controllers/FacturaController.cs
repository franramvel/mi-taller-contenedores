using mi_taller_contenedores.DB;
using mi_taller_contenedores.DB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mi_taller_contenedores.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FacturaController : Controller
    {
        private readonly MainDbContext _ctx;

        public FacturaController(MainDbContext ctx)
        {
            _ctx = ctx;
        }

        //La accion, son los métodos dentro de la clase controller
        [HttpGet("{facturaId}")]
        public IActionResult Get(int facturaId)
        {
            //@Id int
            //select Top(1) * from TblFacturas where Id=@Id
            //La funcion flecha indica el objeto de la tabla que se esta manejando
            //Si no se ejecuta to list, en el caso de varios resultados
            //O FirsrtOrDefault en caso de un solo resultado, no hace nada
            var resultado = _ctx.TblFacturas.Where(factura=>factura.Id== facturaId).FirstOrDefault();
            return Json(resultado);
        }

        //La accion, son los métodos dentro de la clase controller
        [HttpGet("{nombre}")]
        public IActionResult GetByNombre(string razonSocial)
        {
            //@nombre varchar
            //select * from TblFacturas where RazonSocial Like '%@RazonSocial%'
            var resultado = _ctx.TblFacturas.Where(factura => factura.RazonSocial.Contains(razonSocial)).ToList();
            return Json(resultado);
        }

        [HttpGet]
        public IActionResult GetMontosTotales()
        {
            //List es el equivalente a ArrayList en java, o [] en python
            var montos = new List<decimal>();
            //Obtener nuestra lista de facturas
            var facturas = _ctx.TblFacturas
                .ToList();

            foreach (var factura in facturas)
            {
                var calculo = factura.Pasajeros * factura.MontoPorPasajero;
                montos.Add(calculo);
            }

            return Json(montos);
        }

        [HttpGet]
        public IActionResult GetMontosTotalesPaginado(int salto,int tamañoPagina)
        {
            //List es el equivalente a ArrayList en java, o [] en python
            var montos = new List<decimal>();
            //Obtener nuestra lista de facturas
            var facturas = _ctx.TblFacturas.Skip(salto).Take(tamañoPagina)
                .ToList();


            foreach (var factura in facturas)
            {
                var calculo = factura.Pasajeros * factura.MontoPorPasajero;
                montos.Add(calculo);
            }
            

            return Json(montos);
        }

        [HttpGet]
        public IActionResult GetMontosTotalesPaginadoIQueryable(int salto, int tamañoPagina)
        {
            //List es el equivalente a ArrayList en java, o [] en python
            var montos = new List<decimal>();

            IQueryable<Factura> query =
             (from factura in _ctx.TblFacturas
              select new Factura
              {
                  Pasajeros= factura.Pasajeros,
                  MontoPorPasajero= factura.MontoPorPasajero
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


            return Json(montos);
        }

        //La accion, son los métodos dentro de la clase controller
        [HttpPost]
        public IActionResult Insert([FromBody] Factura factura)
        {
            //Cuando llega la factura, aún no esta en la db
            _ctx.TblFacturas.Add(factura);
            //Si no llamamos al metodo save changes, la base de datos no se actualiza
            _ctx.SaveChanges();
            //EF lleva un tracking
            return Json(factura);
        }

    }
}
