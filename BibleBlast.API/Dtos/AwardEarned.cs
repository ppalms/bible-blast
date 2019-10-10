using System;
using System.Collections.Generic;

namespace BibleBlast.API.Dtos
{
    public class AwardEarned
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ItemDescription { get; set; }
        public string Timing { get; set; }
        public IEnumerable<KidAwardListItem> Kids { get; set; }
        public int Ordinal { get; set; }
    }

    public class KidAwardListItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DatePresented { get; set; }
    }
}
