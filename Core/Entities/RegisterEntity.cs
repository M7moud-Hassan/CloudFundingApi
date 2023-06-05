using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RegisterEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperuser { get; set; }
        public string ProfileImg { get; set; }
        public DateTime? Birthdate { get; set; }
        public string FacebookProfile { get; set; }
        public string Country { get; set; }
        public DateTime? LastLogin { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
