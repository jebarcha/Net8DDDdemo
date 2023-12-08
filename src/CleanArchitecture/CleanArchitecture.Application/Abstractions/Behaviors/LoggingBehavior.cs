using CleanArchitecture.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        //Logging all commands that the client send
        var name = request.GetType().Name;
        try
        {
            _logger.LogInformation($"Executing command request: {name}");
            var result = await next();
            _logger.LogInformation($"The command {name} was executed successfully");

            return result;
        }
        catch (System.Exception exception)
        {
            _logger.LogError(exception, $"The command {name} has errors");
            throw;
        }

    }
}
