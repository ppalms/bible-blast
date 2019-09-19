using System;
using System.Collections.Generic;

namespace BibleBlast.API.Dtos
{
    // todo get rid of the CompletedMemories property on KidDetail and use this with a separate resolver for the KidDetail component
    /// <summary>
    /// Viewmodel used to group completed memories by Category and Kid
    /// </summary>
    public class KidMemoryCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<KidMemoryDetail> KidMemories { get; set; }
    }

    /// <summary>
    /// Viewmodel that groups completed memories by Kid
    /// </summary>
    public class KidMemoryDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<CompletedMemoryDetail> Completed { get; set; }
    }

    /// <summary>
    /// Used to display completed memories in the dashboard view
    /// </summary>
    public class CompletedMemoryDetail
    {
        public int MemoryId { get; set; }
        public string MemoryName { get; set; }
        public string MemoryDescription { get; set; }
        public int Points { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
