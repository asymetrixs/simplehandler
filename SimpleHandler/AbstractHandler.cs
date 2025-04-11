using Microsoft.Extensions.Logging;

namespace SimpleHandler;

public abstract class AbstractHandler<TIn, TOut, TPayload>(ILogger<AbstractHandler<TIn, TOut, TPayload>> logger)
    where TIn : class
    where TPayload : class
    where TOut : ResponseModel<TPayload>
{
    public async Task<TOut> Handle(TIn request)
    {
        logger.LogTrace("Authenticating");
        var response = await Authenticate(request);
        if (response.IsError)
        {
            logger.LogTrace("Authenticating failed");
            return response;
        }

        logger.LogTrace("Validating");
        response = await Validate(request);
        if (response.IsError)
        {
            logger.LogTrace("Validating failed");
            return response;
        }

        logger.LogTrace("Processing");
        response = await Process(request);
        logger.LogTrace(response.IsError ? "Processing failed" : "Processing succeeded");

        return response;
    }

    protected abstract Task<TOut> Authenticate(in TIn request);

    protected abstract Task<TOut> Validate(in TIn request);

    protected abstract Task<TOut> Process(in TIn request);
}
