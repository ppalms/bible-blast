namespace BibleBlast.API.Models
{
    public class UserKid
    {
        public int UserId { get; set; }
        public int KidId { get; set; }
        public User User { get; set; }
        public Kid Kid { get; set; }
    }
}
