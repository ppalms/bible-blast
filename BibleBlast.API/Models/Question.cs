using System;
using System.Collections.Generic;

namespace BibleBlast.API.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte Points { get; set; }

        public virtual Category Category { get; set; }
    }
}
