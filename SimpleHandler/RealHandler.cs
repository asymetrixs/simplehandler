using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace SimpleHandler;

public class RealHandler(ILogger<RealHandler> logger)
    : AbstractHandler<RealRequest, ResponseModel<RealResponse>, RealResponse>(logger)
{
    protected override Task<ResponseModel<RealResponse>> Authenticate(in RealRequest request)
    {
        if (request.Something == "abc")
        {
            return Task.FromResult(ResponseModel<RealResponse>.Success());
        }

        return Task.FromResult(ResponseModel<RealResponse>.Failure());
    }


    protected override Task<ResponseModel<RealResponse>> Validate(in RealRequest request)
    {
        if (request.Something == "abc")
        {
            return Task.FromResult(ResponseModel<RealResponse>.Success());
        }

        var response = ResponseModel<RealResponse>.Failure();

        response.ValidationResult = new ValidationResult("Something is wrong");

        return Task.FromResult(response);
    }


    protected override Task<ResponseModel<RealResponse>> Process(in RealRequest request)
    {
        if (request.Something == "abc")
        {
            return Task.FromResult(ResponseModel<RealResponse>.Success());
        }

        return Task.FromResult(ResponseModel<RealResponse>.Failure());
    }
}
