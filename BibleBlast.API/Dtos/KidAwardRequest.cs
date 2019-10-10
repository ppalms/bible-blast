using System;

namespace BibleBlast.API.Dtos
{
    public class KidAwardRequest
    {
        public int KidId { get; set; }
        public int AwardId { get; set; }
        public DateTime DatePresented { get; set; }
    }
}
