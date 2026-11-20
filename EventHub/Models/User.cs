using System;

namespace EventHub.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }   // PBKDF2 hash, never the plain password
        public string Salt { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
