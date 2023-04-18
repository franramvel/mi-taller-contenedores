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
        public async  Task<IActionResult> Get(int facturaId,CancellationToken token)
        {
            var factura = await _service.Get(facturaId,token);
            return Json(factura);
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
        public async Task<IActionResult> GetMontosTotalesPaginado(int salto,int tamañoPagina,CancellationToken cancellation)
        {
            //List es el equivalente a ArrayList en java, o [] en python
            var montos = await _service.GetMontosTotalesPaginado(salto, tamañoPagina, cancellation);
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
            //return Json(_mapper.Map<FacturaFormModel>(facturaIngresada));

            return Json(new BaseResponse<Factura>() { Success=true, Message="Factura Ingresada", Data= facturaIngresada });
        }

        //Ejemplo Generico para un proxy
        public class BaseResponse<T>
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public T Data { get; set; }
        }
    }
}
