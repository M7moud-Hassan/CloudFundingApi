using System.ComponentModel.DataAnnotations;

namespace Core.Identity
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        [Required]
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperuser { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? FacebookProfile { get; set; }
        public string? Country { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}