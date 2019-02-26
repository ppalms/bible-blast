using System;
using System.Collections.Generic;

namespace BibleBlast.API.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int FamilyId { get; set; }
        public DateTime Date { get; set; }
        public decimal Ammount { get; set; }
    }
}
