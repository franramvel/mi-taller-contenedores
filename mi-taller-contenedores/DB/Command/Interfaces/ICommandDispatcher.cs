using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mi_taller_contenedores.DB.Command.Interfaces
{
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Método <c>Dispatch</c> despacha el request según el handler adecuado sin posibilidad a ser cancelado, ya que puede cambiar estado. Se debe especificar en 
        /// los genericos los TCommand y TResult.
        /// <example>
        /// Por ejemplo:
        /// dispatcher.Dispatch&lt;TCommand,TResult&gt;(command); Donde query es del tipo TQuery.
        /// </example>
        /// </summary>
        Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand query);
    }
}
