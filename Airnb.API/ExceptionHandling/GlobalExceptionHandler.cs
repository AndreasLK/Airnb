using Airnb.Application.Exceptions;
using Airnb.Application.Exceptions.Airnb.Application.Exceptions;
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
                InvalidCredentialsException => (StatusCodes.Status401Unauthorized, "Ikke logget ind"),
                InvalidOperationException => (StatusCodes.Status400BadRequest, "Ugyldig operation"),
                UnauthorizedAccessException => (StatusCodes.Status403Forbidden, "Ingen adgang"),
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Ikke fundet"),
                DomainException => (StatusCodes.Status400BadRequest, "Forretningsregelovertrådt"),
                ConflictException => (StatusCodes.Status409Conflict, "Konflikt"),
                ArgumentException => (StatusCodes.Status400BadRequest, "Ugyldigt input"),
                NotFoundException => (StatusCodes.Status404NotFound, "Ikke fundet"),
                ForbiddenException => (StatusCodes.Status403Forbidden, "Ingen adgang"),
                ValidationException => (StatusCodes.Status400BadRequest, "Valideringsfejl"),
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
