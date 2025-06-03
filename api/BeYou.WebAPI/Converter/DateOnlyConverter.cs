using Newtonsoft.Json;
using System.Globalization;

namespace BeYou.WebAPI.Converter;

/// <summary>
/// Converter class to accept string as date only
/// </summary>
public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    /// <summary>
    /// Read specific string from json and treat as date only
    /// </summary>
    /// <param name="reader">Json reader</param>
    /// <param name="objectType">Type</param>
    /// <param name="existingValue">Existing value as date only</param>
    /// <param name="hasExistingValue">Indicates if there is existing value</param>
    /// <param name="serializer">Json serializer options</param>
    /// <returns>DateOnly</returns>
    public override DateOnly ReadJson(JsonReader reader,
        Type objectType,
        DateOnly existingValue,
        bool hasExistingValue,
        JsonSerializer serializer) =>
        DateOnly.ParseExact((string)reader.Value!, Format, CultureInfo.InvariantCulture);

    /// <summary>
    /// Override the write json from writer
    /// </summary>
    /// <param name="writer">Json writer</param>
    /// <param name="value">Date only value</param>
    /// <param name="serializer">Json serializer options</param>
    public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer) =>
        writer.WriteValue(value.ToString(Format, CultureInfo.InvariantCulture));
}