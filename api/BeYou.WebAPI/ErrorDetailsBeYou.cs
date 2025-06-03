using BeYou.Utils;

namespace BeYou.WebAPI;

public class ErrorDetailsBeYou
{
    /// <summary>
    /// Type identifier
    /// </summary>
    /// <value></value>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Status code for error
    /// </summary>
    /// <value></value>
    public int StatusCode { get; set; }

    /// <summary>
    /// Error message
    /// </summary>
    /// <value></value>
    public string? Message { get; set; }

    /// <summary>
    /// Error detail
    /// </summary>
    /// <value></value>
    public string? Detail { get; set; }

    /// <summary>
    /// Log level property
    /// </summary>
    /// <value></value>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    /// Overrided method to string to serialize
    /// </summary>
    /// <returns>string</returns>
    public override string ToString() => Serialization.Serialize(this);
}