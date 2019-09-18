using System.Collections.Generic;
using BibleBlast.API.Models;

public class Award
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int ItemId { get; set; }
    public bool IsImmediate { get; set; }
    public int Ordinal { get; set; }
    public MemoryCategory Category { get; set; }
    public AwardItem Item { get; set; }
    public ICollection<AwardMemory> AwardMemories { get; set; }
}
