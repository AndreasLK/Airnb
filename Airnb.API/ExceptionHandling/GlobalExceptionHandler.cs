using Airnb.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
using static System.Net.WebRequestMethods;


namespace Airnb.API.ExceptionHandling
{
   

public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetails;

        public GlobalExceptionHandler(IProblemDetailsService problemDetails)
        {
            _problemDetails = problemDetails;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (statusCode, title) = exception switch
            {
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Ikke fundet"),
                DomainException => (StatusCodes.Status400BadRequest, "Forretningsregelovertrådt"),
                ArgumentException => (StatusCodes.Status400BadRequest, "Ugyldigt input"),
                _ => (0, "")
            };

            if (statusCode == 0)
                return false; // ukendt fejl → standard 500

            httpContext.Response.StatusCode = statusCode;

            return await _problemDetails.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Title = title,
                    Detail = exception.Message,
                    Status = statusCode
                }
            });
        }
    }
}
