namespace sposko;

public class CreateSportGroupDTO
{
    public string? Name { get; set; }
    public Guid? TrainerId { get; set; }
    public int SportId { get; set; }
    public int? MaxMembers { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
}

