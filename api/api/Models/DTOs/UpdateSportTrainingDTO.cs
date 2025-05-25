namespace sposko;

public class UpdateSportTrainingDTO
{
    public int? GroupId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTimeOffset? StartTime { get; set; }
    public TimeSpan? Duration { get; set; }
    public DateTime? EndDate { get; set; }
    public string? RepeatType { get; set; }
    public int? RepeatInterval { get; set; }
    public decimal? Cost { get; set; }
}
