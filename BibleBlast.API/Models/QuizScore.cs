using System;
using System.Collections.Generic;

namespace BibleBlast.API.Models
{
    public class QuizScore
    {
        public int KidId { get; set; }
        public DateTime Date { get; set; }
        public byte Points { get; set; }
    }
}
