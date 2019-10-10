using System;

namespace BibleBlast.API.Models
{
    public class KidAward
    {
        public int KidId { get; set; }
        public int AwardId { get; set; }
        public Kid Kid { get; set; }
        public Award Award { get; set; }
        public DateTime DatePresented { get; set; } = DateTime.Today;
    }
}
