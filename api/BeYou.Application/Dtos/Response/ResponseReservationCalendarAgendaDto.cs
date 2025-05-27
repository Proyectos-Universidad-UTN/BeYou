namespace BeYou.Application.Dtos.Response;

public record ResponseReservationCalendarAgendaDto
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string ClassNames { get; set; } = "fc-bg-default";

    public string Icon { get; set; } = "calendar";

    public bool AllDay { get; set; } = false;

    public string? Display { get; set; }
}
