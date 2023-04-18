using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mi_taller_contenedores.DB.Query.Interfaces
{


    public interface IQueryHandler<in TQuery, TQueryResult>
    {
        Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
    }

    //Ejemplo para interfaces
    //public class Pecera
    //{
    //    public void NadarPeces(Pez pez)
    //    {
    //        pez.Nadando();
    //    }

    //}

    //public interface Pez
    //{
    //    public void Nadando();
    //}

    //class PecesAguaDulce:Pez
    //{
    //    public void Nadando()
    //    {
    //        Console.WriteLine("Nadaneo en agua dulce");
    //    }

    //}

    //class PecesAguaSalada:Pez
    //{
    //    public void Nadando()
    //    {
    //        Console.WriteLine("Nadaneo en agua salada");
    //    }
    //}
}