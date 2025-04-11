using Microsoft.Extensions.Logging;

namespace SimpleHandler;

public abstract class AbstractHandler<TIn, TOut, TPayload>(ILogger<AbstractHandler<TIn, TOut, TPayload>> logger)
    where TIn : class
    where TPayload : class
    where TOut : ResponseModel<TPayload>
{
    public async Task<TOut> Handle(TIn request)
    {
        var response = await Authenticate(request);

        if (response.IsError)
        {
            return response;
        }

        response = await Validate(request);

        if (response.IsError)
        {
            return response;
        }

        return await Process(request);
    }

    protected virtual Task<TOut> Authenticate(in TIn request)
    {
        logger.LogWarning("Authenticate not implemented");
        return Task.FromResult(Activator.CreateInstance<TOut>());
    }

    protected virtual Task<TOut> Validate(in TIn request)
    {
        logger.LogWarning("Validate not implemented");
        return Task.FromResult(Activator.CreateInstance<TOut>());
    }

    protected virtual Task<TOut> Process(in TIn request)
    {
        logger.LogWarning("Process not implemented");
        return Task.FromResult(Activator.CreateInstance<TOut>());
    }
}
