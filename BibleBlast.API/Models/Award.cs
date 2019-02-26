using System;
using System.Collections.Generic;

namespace BibleBlast.API.Models
{
    public class Award
    {
        public int AwardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}
