using System;
using System.ComponentModel.DataAnnotations;

namespace EventHub.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int EventId { get; set; }

        [Required(ErrorMessage = "Enter your full name")]
        [StringLength(80)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter your email")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your phone number")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a 10-digit Indian mobile number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Enter your college / university")]
        public string College { get; set; }

        [Required(ErrorMessage = "Enter your course, e.g. B.Tech CSE")]
        public string Course { get; set; }

        [Range(1, 6, ErrorMessage = "Year must be between 1 and 6")]
        public int Year { get; set; }

        public DateTime RegisteredOn { get; set; }
        public string Status { get; set; }        // Confirmed / Cancelled
    }
}
