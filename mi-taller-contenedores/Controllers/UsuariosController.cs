using AutoMapper;
using mi_taller_contenedores.DB;
using mi_taller_contenedores.DB.Model;
using Microsoft.AspNetCore.Mvc;

namespace mi_taller_contenedores.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsuariosController : Controller
    {
        private readonly IMapper _mapper;

        public UsuariosController(IMapper mapper)
        {
            _mapper = mapper;
        }

        //Para inserciones se va a utilizar HttpPost
        [HttpPost]
        public IActionResult Insert([FromBody] UsuarioFormModel model)
        {
            var usuario = _mapper.Map<Usuario>(model);
            return Ok("Tu usuario se ha registrado");
        }

        [HttpGet]
        public IActionResult GetFront(int usuarioId)
        {
            //Este objeto nos lo regresa un servicio que consulta a la db
            var usuario = new Usuario()
            {
                Id = usuarioId,
                Password = "ASDKJASBDNKJ4KJ5L3B45KLJ3",
                Rol = new Rol() { Id = 1, Nombre = "Usuario" },
                UserName = "TEST"
            };

            var respuesta = _mapper.Map<UsuarioResponseFrontModel>(usuario);
            return Json(respuesta);
        }

        [HttpGet]
        public IActionResult GetWS(int usuarioId)
        {
            //Este objeto nos lo regresa un servicio que consulta a la db
            var usuario = new Usuario()
            {
                Id = usuarioId,
                Password = "ASDKJASBDNKJ4KJ5L3B45KLJ3",
                Rol = new Rol() { Id = 1, Nombre = "Usuario" },
                UserName = "TEST"
            };

            var respuesta = _mapper.Map<UsuarioResponseWSModel>(usuario);
            return Json(respuesta);
        }






    }
}
