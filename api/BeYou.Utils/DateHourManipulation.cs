using System.Globalization;

namespace BeYou.Utils;

public static class DateHourManipulation
{
    /// <summary>
    /// Get hours from specific period of time
    /// </summary>
    /// <param name="startTime">Start time</param>
    /// <param name="endTime">End time</param>
    /// <returns>List<TimeOnly></returns>
    public static List<TimeOnly> GetHoursAsync(TimeOnly startTime, TimeOnly endTime)
    {
        List<TimeOnly> horas = new List<TimeOnly>();

        TimeOnly tiempoActual = startTime;

        while (tiempoActual <= endTime)
        {
            horas.Add(tiempoActual);

            tiempoActual = tiempoActual.AddHours(1);
        }

        return horas;
    }

    /// <summary>
    /// Get days from specific period of days
    /// </summary>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>List<DateOnly></returns>
    public static List<DateOnly> GetDaysAsync(DateOnly startDate, DateOnly endDate)
    {
        List<DateOnly> days = new List<DateOnly>();

        DateOnly diaActual = startDate;

        while (diaActual <= endDate)
        {
            days.Add(diaActual);

            diaActual = diaActual.AddDays(1);
        }

        return days;
    }

    /// <summary>
    /// Get specific days of week name for Costa Rica culture info
    /// </summary>
    /// <param name="date">Date to look for day of week</param>
    /// <returns>string</returns>
    public static string GetDayWeekCultureCostaRica(DateOnly date) => new CultureInfo("es-CR").DateTimeFormat.GetDayName(date.DayOfWeek).Capitalize().Replace("é", "e").Replace("á", "a");

}
