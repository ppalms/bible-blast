using System;

namespace BibleBlast.API.Dtos
{
    /// <summary>
    /// Maps to the body of POST and DELETE requests sent to <c>api/kids/{id}/memories</c>
    /// and can be automapped to the <see cref="KidMemory"/> type
    /// </summary>
    public class KidMemoryRequest
    {
        public int MemoryId { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
