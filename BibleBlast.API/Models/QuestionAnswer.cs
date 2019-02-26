using System;
using System.Collections.Generic;

namespace BibleBlast.API.Models
{
    public class QuestionAnswer
    {
        public int KidId { get; set; }
        public int QuestionId { get; set; }
        public DateTime Date { get; set; }
        public string SubmittedBy { get; set; }
    }
}
