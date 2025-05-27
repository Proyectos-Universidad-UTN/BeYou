using System.Text;

namespace BeYou.Common.Helper;

public static class ErrorHandling
{
    /// <summary>
    /// Gets the error message from an exception.
    /// </summary>
    /// <param name="ex">Exception generated</param>
    /// <returns>Entire description of the error message</returns>
    public static string? GetErrorMessage(Exception ex)
    {
        if (ex == null) return null;

        var errorMessage = new StringBuilder();
        errorMessage.Append(ex.Message);
        return ex.InnerException != null ? GetAllErrorMessage(ex) : errorMessage.ToString();
    }

    /// <summary>
    /// Gets the entire error message from an exception, including inner exceptions.
    /// </summary>
    /// <param name="ex">Exception generated</param>
    /// <returns>Entire description of the error message</returns>
    private static string GetAllErrorMessage(Exception ex) => ex.InnerException == null ? ex.Message : $"{ex.Message} : {GetAllErrorMessage(ex.InnerException)}";
}
