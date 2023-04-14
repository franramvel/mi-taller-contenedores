using AutoMapper;
using mi_taller_contenedores.DB;
using mi_taller_contenedores.DB.Model;
using Microsoft.AspNetCore.Mvc;

namespace mi_taller_contenedores.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PaypalController : Controller
    {
        private readonly IMapper _mapper;

        public PaypalController(IMapper mapper)
        {
            _mapper = mapper;
        }

        //Para inserciones se va a utilizar HttpPost
        [HttpPost]
        public IActionResult WebhookCatch([FromBody] PagoPaypalRequestModel model)
        {
            var pago = _mapper.Map<PagoPaypal>(model);
            return Ok("Tu usuario se ha registrado");
        }


    }
}
