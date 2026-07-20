namespace Domain.Dto.Session;

public class PlayStatistics
{
    public int DefeatedEnemies { get; set; }
    public int TotalExperience { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public TimeSpan Duration => EndDate - StartDate;
}
