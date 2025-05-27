using System.Globalization;
using Newtonsoft.Json;

namespace BeYou.Utils.Converter;

/// <summary>
/// Json converter for DateOnly type.
/// </summary>
public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    /// <summary>
    /// Reads the JSON representation of a DateOnly value. 
    /// </summary>
    /// <param name="reader">JSON reader</param>
    /// <param name="objectType">Object type</param>
    /// <param name="existingValue">Date existing value</param>
    /// <param name="hasExistingValue">Identify is has a value</param>
    /// <param name="serializer">JSON serializer</param>
    /// <returns>Date converted</returns>
    public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        => DateOnly.ParseExact((string)reader.Value!, Format, CultureInfo.InvariantCulture);

    /// <summary>
    /// Writes the JSON representation of a DateOnly value.
    /// </summary>
    /// <param name="writer">JSON writer</param>
    /// <param name="value">Date value</param>
    /// <param name="serializer">JSON serializer</param>
    public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer) =>
        writer.WriteValue(value.ToString(Format, CultureInfo.InvariantCulture));
}