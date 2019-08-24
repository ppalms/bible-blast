using System;
using System.Collections.Generic;

public class KidMemoryQueryParams
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public IEnumerable<int> CategoryIds { get; set; }
}
