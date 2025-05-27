using Newtonsoft.Json;
using System.Globalization;

namespace BeYou.Utils.Converter;

/// <summary>
/// Json converter for TimeOnly type.
/// </summary>
public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    private const string Format = "HH:mm";

    /// <summary>
    /// Reads the JSON representation of a TimeOnly value.
    /// </summary>
    /// <param name="reader">JSON reader</param>
    /// <param name="objectType">Object type</param>
    /// <param name="existingValue">Existing time value</param>
    /// <param name="hasExistingValue">Identify if has a value</param>
    /// <param name="serializer">JSON serializer</param>
    /// <returns>Time converted</returns> 
    public override TimeOnly ReadJson(JsonReader reader, Type objectType, TimeOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
       => TimeOnly.ParseExact((string)reader.Value!, Format, CultureInfo.InvariantCulture);

    /// <summary>
    /// Writes the JSON representation of a TimeOnly value. 
    /// </summary>
    /// <param name="writer">JSON writer</param>
    /// <param name="value">Time value</param>
    /// <param name="serializer">JSON serializer</param>
    public override void WriteJson(JsonWriter writer, TimeOnly value, JsonSerializer serializer) =>
        writer.WriteValue(value.ToString(Format, CultureInfo.InvariantCulture));
}
