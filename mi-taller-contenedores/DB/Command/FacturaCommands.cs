using mi_taller_contenedores.DB.Model;
using mi_taller_contenedores.DB.Query.Interfaces;
using mi_taller_contenedores.DB.Query;
using DB.Command.Interfaces;

namespace mi_taller_contenedores.DB.Command
{
    public record CommandInsertFacturacion(Factura factura);
    public record CommandUpdateFacturacion(Factura factura);
    public record CommandUpdatePasajeros(int id,int pasajeros);

    public interface IFacturacionCommandHandler :
    ICommandHandler<CommandInsertFacturacion, Factura>,//Esta interfaz se declara por cada query  que vayamos a usar
    ICommandHandler<CommandUpdateFacturacion, Factura>,//Esta interfaz se declara por cada query  que vayamos a usar
    ICommandHandler<CommandUpdatePasajeros, (int, string)>
    //Int representa respuesta http, string representa mensaje de error
    {
    }


    public class FacturaCommands : IFacturacionCommandHandler
    {
        private readonly MainDbContext _ctx;

        public FacturaCommands(MainDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Factura> Handle(CommandInsertFacturacion command)
        {
            _ctx.TblFacturas.Add(command.factura);
            //Si no llamamos al metodo save changes, la base de datos no se actualiza
            _ctx.SaveChanges();
            //EF lleva un tracking
            return command.factura;
        }

        public async Task<Factura> Handle(CommandUpdateFacturacion command)
        {
            var facturaaActualizar = _ctx.TblFacturas.Where(factura => factura.Id == command.factura.Id).FirstOrDefault();
            if (facturaaActualizar!=null)//Si el objeto existe, EF lo crea, por eso ponemos este if
            {
                //UPDATE, Si no lo encuentra, lo crea
                //El objeto facturaaActualizar NO es igual al objeto command.factura
                //_ctx.TblFacturas.Update(command.factura); //Esto genera conflicto, porque ambos tienen el mismo id
                //facturaaActualizar.Pasajeros = command.factura.Pasajeros;
                //...


                _ctx.TblFacturas.Update(command.factura);
                //Si no llamamos al metodo save changes, la base de datos no se actualiza
                _ctx.SaveChanges();
                //EF lleva un tracking
            }

            return facturaaActualizar;
        }

        public async Task<(int, string)> Handle(CommandUpdatePasajeros command)
        {
            //buscar el registro
            var facturaaActualizar= _ctx.TblFacturas.Where(factura => factura.Id == command.id).FirstOrDefault();

            if (facturaaActualizar == null)
            {
                return (404, "No se encontro la factura");
            }

            facturaaActualizar.Pasajeros = command.pasajeros;

            _ctx.TblFacturas.Update(facturaaActualizar);
            //Si no llamamos al metodo save changes, la base de datos no se actualiza
            _ctx.SaveChanges();

            //EF lleva un tracking
            return (200,"Se actualizo tu factura correctamente");

        }
    }
}

