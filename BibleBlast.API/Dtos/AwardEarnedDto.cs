using System;

public class AwardEarnedDto
{
    public int AwardId { get; set; }
    public int CategoryId { get; set; }
    public string ItemDescription { get; set; }
    public bool IsImmediate { get; set; }
    public int KidId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DatePresented { get; set; }
    public int Ordinal { get; set; }
}
