using System.Text;

namespace BeYou.WebAPI.Configuration;

/// <summary>
/// Error handling configuration class
/// </summary>
public static class ErrorHandling
{
    /// <summary>
    /// Get message exception
    /// </summary>
    /// <param name="exception">Exception handled</param>
    /// <returns>string</returns>
    public static string? GetMessageException(Exception exception)
    {
        if (exception == null) return null;

        var errorMessage = new StringBuilder();
        errorMessage.Append(exception.Message);
        return exception.InnerException != null ? GetInnerMessageExceptions(exception) : errorMessage.ToString();
    }

    /// <summary>
    /// Get the inner message exception
    /// </summary>
    /// <param name="exception">Exception handled</param>
    /// <returns>string</returns>
    private static string GetInnerMessageExceptions(Exception exception)
    {
        if (exception.InnerException == null) return exception.Message;

        return $"{exception.Message} : {GetInnerMessageExceptions(exception.InnerException)}";
    }
}