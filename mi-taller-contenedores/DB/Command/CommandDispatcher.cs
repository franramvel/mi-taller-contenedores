using DB.Command.Interfaces;

namespace mi_taller_contenedores.DB.Command
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand query)
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();
            return await handler.Handle(query);
        }
    }
}
