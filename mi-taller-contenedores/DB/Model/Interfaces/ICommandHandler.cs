using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Command.Interfaces
{

        public interface ICommandHandler<in TCommand, TCommandResult>
        {
            Task<TCommandResult> Handle(TCommand command);
        }
    
}
