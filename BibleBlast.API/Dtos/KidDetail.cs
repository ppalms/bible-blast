using System;
using System.Collections.Generic;

namespace BibleBlast.API.Dtos
{
    /// <summary>
    /// DTO that maps from Kid and contains related entites such as Parents and Completed Memories. 
    /// To fully map this DTO, be sure to use <see cref="IKidRepository.GetKidWithChildEntities"/>.
    /// </summary>
    public class KidDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Grade { get; set; }
        public DateTime? Birthday { get; set; }
        public ICollection<UserDetail> Parents { get; set; }
        public DateTime DateRegistered { get; set; }
        public ICollection<CompletedMemory> CompletedMemories { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Used in the kid detail view where users can view/edit completed memories
    /// </summary>
    public class CompletedMemory
    {
        public int KidId { get; set; }
        public int MemoryId { get; set; }
        public int? Points { get; set; }
        public int CategoryId { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}
