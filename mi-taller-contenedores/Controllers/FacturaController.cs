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
        private readonly IFacturaServices _service;
        private readonly IMapper _mapper;

        public FacturaController(IFacturaServices service,IMapper mapper)
        {
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
        [HttpGet("{razonSocial}")]
        public async Task<IActionResult> GetByNombre(string razonSocial,CancellationToken cancellation)
        {
            //@nombre varchar
            //select * from TblFacturas where RazonSocial Like '%@RazonSocial%'
            var factura = await _service.GetByRazonSocial(razonSocial, cancellation);
            return Json(factura);
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
        public async Task<IActionResult> Insert([FromBody] FacturaFormModel facturaModel)
        {

            //var factura = new Factura()
            //{
            //    RazonSocial = facturaModel.RazonSocial,
            //    RFC = facturaModel.RFC,
            //    Pasajeros = facturaModel.Pasajeros,
            //    MontoPorPasajero = facturaModel.MontoPorPasajero
            //};
            var factura = _mapper.Map<Factura>(facturaModel);
            var facturaIngresada = await _service.Insert(factura);
            return Json(_mapper.Map<FacturaFormModel>(facturaIngresada));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] FacturaFormModel facturaModel)
        {

            //var factura = new Factura()
            //{
            //    RazonSocial = facturaModel.RazonSocial,
            //    RFC = facturaModel.RFC,
            //    Pasajeros = facturaModel.Pasajeros,
            //    MontoPorPasajero = facturaModel.MontoPorPasajero
            //};
            var factura = _mapper.Map<Factura>(facturaModel);
            var facturaActualizada = await _service.Update(factura);
            if (facturaActualizada==null)
            {
                return NotFound("La factura que intentas actualizar no existe");
            }
            else
            {
                //Respuesta JSON trae implicitamente codigo OK 200
                return Json(_mapper.Map<FacturaFormModel>(facturaActualizada));
            }
            
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePasajeros(int id, [FromBody] int pasajeros)
        {


            var codeMessage = await _service.UpdatePasajeros(id,pasajeros);
            //Item1 trae el codigo de respuesta, Item 2 trae el mensaje
            switch (codeMessage.Item1)
            {
                case 404:
                    return NotFound(codeMessage.Item2);
                    break;
                //case 409:
                //    return Conflict(codepath.Item2);
                default:
                    return Ok(codeMessage.Item2);
                    // Abre el archivo utilizando FileStream
            }
        }


        //Ejemplo Generico para un proxy
        //public class BaseResponse<T>
        //{
        //    public bool Success { get; set; }
        //    public string Message { get; set; }
        //    public T Data { get; set; }
        //}
    }
}
