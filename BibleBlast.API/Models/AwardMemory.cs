using BibleBlast.API.Models;

public class AwardMemory
{
    public int AwardId { get; set; }
    public int MemoryId { get; set; }
    public Award Award { get; set; }
    public Memory Memory { get; set; }
}
