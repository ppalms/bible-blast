using System;
using System.Collections.Generic;

namespace BibleBlast.API.Models
{
    public class KidMemory
    {
        public int KidId { get; set; }
        public int MemoryId { get; set; }
        public Kid Kid { get; set; }
        public Memory Memory { get; set; }
        public DateTime DateCompleted { get; set; } = DateTime.Today;

        public override bool Equals(object obj)
        {
            if (!(obj is KidMemory kidMemory))
            {
                return false;
            }

            return this.KidId == kidMemory.KidId && this.MemoryId == kidMemory.MemoryId;
        }

        public override int GetHashCode()
        {
            int code = this.KidId ^ this.MemoryId;
            return code.GetHashCode();
        }
    }
}
