using BeYou.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BeYou.WebAPI.Configuration;

/// <summary>
/// Exception handling configuration extension class
/// </summary>
public static class ExceptionHandlingConfigurationExtension
{
    /// <summary>
    /// Configuration exception handler extension to catch errors
    /// /// </summary>
    /// <param name="app">Application builder</param>
    /// <param name="logger">App log</param>
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, Serilog.ILogger logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFailure = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFailure != null)
                {
                    var errorDetails = GetErrorDetails(contextFailure);

                    // Log the error using Serilog
                    logger.Error(contextFailure.Error, "An error occurred: {ErrorDetails}", errorDetails);

                    context.Response.StatusCode = errorDetails.StatusCode;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(errorDetails.ToString());
                }
            });
        });
    }

    /// <summary>
    /// Get details of the error thrown
    /// </summary>
    /// <param name="exception">Exception handler feature</param>
    /// <returns>ErrorDetailsBeYou</returns>
    private static ErrorDetailsBeYou GetErrorDetails(IExceptionHandlerFeature exception)
    {
        HttpStatusCode httpStatusCode;
        LogLevel logLevel;
        switch (exception.Error)
        {
            case NotFoundException d:
                httpStatusCode = d.HttpStatusCode;
                logLevel = d.LogLevel;
                break;
            case BeYouException e:
                httpStatusCode = e.HttpStatusCode;
                logLevel = e.LogLevel;
                break;
            case UnAuthorizedException f:
                httpStatusCode = f.HttpStatusCode;
                logLevel = f.LogLevel;
                break;
            case ValidationException:
            case FluentValidation.ValidationException:
            case ValidationEntityException:
                httpStatusCode = HttpStatusCode.UnprocessableEntity;
                logLevel = LogLevel.Information;
                break;
            case ListNotAddedException g:
                httpStatusCode = g.HttpStatusCode;
                logLevel = g.LogLevel;
                break;
            case BadRequestException:
                httpStatusCode = HttpStatusCode.BadRequest;
                logLevel = LogLevel.Information;
                break;
            default:
                httpStatusCode = HttpStatusCode.MethodNotAllowed;
                logLevel = LogLevel.Error;
                break;
        }

        var errorDetails = new ErrorDetailsBeYou()
        {
            Type = exception.Error.GetType().Name,
            StatusCode = (int)httpStatusCode,
            Message = ErrorHandling.GetMessageException(exception.Error),
            Detail = exception.Error.StackTrace,
            LogLevel = logLevel
        };

        return errorDetails;
    }
}