using AutoMapper;
using mi_taller_contenedores.ApiModels;
using mi_taller_contenedores.DB;
using mi_taller_contenedores.DB.Model;
using mi_taller_contenedores.Servicios.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mi_taller_contenedores.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FacturaController : Controller
    {
        private readonly MainDbContext _ctx;
        private readonly IFacturaServices _service;
        private readonly IMapper _mapper;

        public FacturaController(MainDbContext ctx,IFacturaServices service,IMapper mapper)
        {
            _ctx = ctx;
            _service = service;
            _mapper = mapper;
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

            var montos = _service.GetMontosTotalesPaginadoIQueryable(salto, tamañoPagina);
            return Json(montos);
        }

        //La accion, son los métodos dentro de la clase controller
        //Queremos obtener la informacion del form, que excluya los campos UUID y PathTestigo en el request, pero no en el response
        [HttpPost]
        public IActionResult Insert([FromBody] FacturaFormModel facturaModel)
        {

            //var factura = new Factura()
            //{
            //    RazonSocial = facturaModel.RazonSocial,
            //    RFC = facturaModel.RFC,
            //    Pasajeros = facturaModel.Pasajeros,
            //    MontoPorPasajero = facturaModel.MontoPorPasajero
            //};
            var factura = _mapper.Map<Factura>(facturaModel);
            var facturaIngresada = _service.Insert(factura);
            return Json(_mapper.Map<FacturaFormModel>(facturaIngresada));
        }

    }
}
