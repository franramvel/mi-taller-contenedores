using mi_taller_contenedores.DB;
using mi_taller_contenedores.DB.Model;
using Microsoft.AspNetCore.Mvc;

namespace mi_taller_contenedores.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HelloWorldController:Controller
    {

        //La accion, son los métodos dentro de la clase controller
        [HttpGet]
        public string Respuesta()
        {
            
            return "Hola Mundo";
        }

        //En la anotacion [HttpGet] se pueden poner los mismos requests de Http convencional
        [HttpGet]
        public IActionResult RespuestaJson()
        {


            return Json(new RespuestaJsonModel() { FechaSolicitud=DateTime.Now,Mensaje="Hello World"});
        }

        //EN MVC no se permite body en metodo Get, Options
        //Para ese caso, se obtienen los parametros del query
        //Para el consumo se veria de la forma baseurl/HelloWorld/ObtenerRegistroPorId/id=1
        [HttpGet("{id}")]
        public IActionResult ObtenerRegistroPorId(int id)
        {
            var mensaje= $"El id del registro es {id}";
            return Json(new RespuestaJsonModel() { FechaSolicitud = DateTime.Now, Mensaje = mensaje });
        }

        [HttpPost]
        public IActionResult RegistrarMensaje([FromBody]string mensaje)
        {
            
            return Json(new RespuestaJsonModel() { FechaSolicitud = DateTime.Now, Mensaje = $"Se registro tu mensaje: '{mensaje}'" });
        }

        //Para inserciones se va a utilizar HttpPost
        [HttpPost]
        public IActionResult RegistrarMensajeJson([FromBody] RequestMensajeModel model)
        {

            return Json(new RespuestaJsonModel() { FechaSolicitud = DateTime.Now, Mensaje = $"Se registro el mensaje de {model.Remitente} y éste es: '{model.Mensaje}'" });
        }

        //Para actualizaciones se va a utilizar HttpPut
        [HttpPut]
        public IActionResult ActualizarMensajeJson([FromBody] RequestMensajeModel model)
        {
            return Json(new RespuestaJsonModel() { FechaSolicitud = DateTime.Now, Mensaje = $"Se actualizó el mensaje de {model.Remitente} y el nuevo mensaje es: '{model.Mensaje}'" });
        }

        //Caso de uso:
        //Que quieran actualizar un registro que no existe
        [HttpPut]
        public IActionResult ActualizarRegistro([FromBody] RequestActualizarRegistro model)
        {
            //Aqui iria la logica de actualizacion
            //Estamos simulando que no se encontro el registro a actualizar, por eso es null
            string? registroActualizado = "test";

            if (registroActualizado == null)
            {
                //NotFound es http 404
                return NotFound("El registro que intentas actualizar no existe");
            }
            else
            {
                //Ok es http 200
                return Ok("El registro se actualizo correctamente");
            }
        }


    }
}
