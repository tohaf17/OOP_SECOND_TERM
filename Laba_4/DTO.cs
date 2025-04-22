namespace Laba_4;

public class PerformerDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
}

public class PerformanceDTO
{
    public Work Work { get; set; }
    public PerformerDTO Performer { get; set; }
    public int Duration { get; set; }
    public string Title { get; set; }
}

public class ConcertDTO
{
    public string Organizer { get; set; }
    public DateTime Date { get; set; }
    public List<PerformanceDTO> Performances { get; set; } = new();
}