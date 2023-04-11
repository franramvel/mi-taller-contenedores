using mi_taller_contenedores.DB;
using mi_taller_contenedores.DB.Model;
using Microsoft.AspNetCore.Mvc;

namespace mi_taller_contenedores.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FacturaController : Controller
    {

        //La accion, son los métodos dentro de la clase controller
        [HttpGet]
        public string GetFactura()
        {

            return "Esto esuna factura";
        }


    }
}
