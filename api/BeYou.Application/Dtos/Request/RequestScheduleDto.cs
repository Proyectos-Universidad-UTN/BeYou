using BeYou.Application.Enums;
namespace BeYou.Application.Dtos.Request;
public record RequestScheduleDto : RequestBaseDto
{
    public WeekDayApplication Day { get; set; }

    public TimeOnly StartHour { get; set; }

    public TimeOnly EndHour { get; set; }
}