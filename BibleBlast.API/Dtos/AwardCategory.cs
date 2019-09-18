using System.Collections.Generic;

namespace BibleBlast.API.Dtos
{
    public class AwardCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<AwardListItem> Awards { get; set; }
    }
}
