using mi_taller_contenedores.DB.Model;
using mi_taller_contenedores.DB;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace mi_taller_contenedores
{
    public class Seeder
    {
        private readonly MainDbContext _ctx;
        private readonly IWebHostEnvironment _hosting;


        public Seeder(MainDbContext ctx, IWebHostEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;

        }

        public async Task SeedAsync()
        {

            //await FillNotificaciones();
            await FillFacturas();

        }

        private async Task FillFacturas()
        {
            if (!_ctx.TblFacturas.Any())
            {
                //Create Sample Data
                var path = Path.Combine(_hosting.ContentRootPath, "DataJson/Facturas.json");
                var json = File.ReadAllText(path);
                var objetos = JsonConvert.DeserializeObject<IEnumerable<Factura>>(json);
                _ctx.TblFacturas.AddRange(objetos);
                _ctx.SaveChanges();
            }
        }
    }
}
